using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace GNF.Common.Utility
{
    /// <summary>
    /// 快速反射方法辅助类
    /// </summary>
    public class FastMethodUtility
    {
        /// <summary>
        /// 动态执行方法
        /// </summary>
        /// <param name="implement"></param>
        /// <param name="methodName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public object Execute<TImplement>(TImplement implement, string methodName, params object[] args)
        {
            var methodInfo = GetMethod(typeof(TImplement), methodName, args);
            if (methodInfo == null)
            {
                throw new Exception($"no find methodName:{methodName}");
            }
            var invokeHandler = GetMethodInvoker(methodInfo);
            if (invokeHandler == null) throw new Exception($"no find delegate method:{methodName}");
            return invokeHandler.Invoke(implement, args);
        }

        /// <summary>
        /// 动态执行方法
        /// </summary>
        /// <param name="implementType"></param>
        /// <param name="methodName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public object Execute(Type implementType, string methodName, params object[] args)
        {
            var methodInfo = GetMethod(implementType, methodName, args);
            if (methodInfo == null)
            {
                throw new Exception($"no find methodName:{methodName}");
            }
            var invokeHandler = GetMethodInvoker(methodInfo);
            if (invokeHandler == null) throw new Exception($"no find delegate method:{methodName}");
            return invokeHandler.Invoke(implementType, args);
        }

        /// <summary>
        /// 获取函数方法对象
        /// </summary>
        /// <param name="t">对象类型，typeOf(class)</param>
        /// <returns></returns>
        public static MethodInfo[] GetMethods(Type t)
        {
            try
            {
                return t.GetMethods();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取函数方法对象
        /// </summary>
        /// <param name="t">对象类型，typeOf(class)</param>
        /// <param name="methodName">函数方法名称</param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static MethodInfo GetMethod(Type t, string methodName, params object[] parameters)
        {
            try
            {
                return t.GetMethod(methodName);
            }
            catch
            {
                try
                {
                    return t.GetMethod(methodName, (from p in parameters where p != null select p.GetType()).ToArray());
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 反射委托事件
        /// </summary>
        /// <param name="target">目标类型对象</param>
        /// <param name="paramters">参数</param>
        /// <returns></returns>
        public delegate object FastInvokeHandler(object target, object[] paramters);

        /// <summary>
        /// 获取原方法事件委托
        /// </summary>
        /// <param name="methodInfo">函数方法信息</param>
        /// <returns></returns>
        public static FastInvokeHandler GetMethodInvoker(MethodInfo methodInfo)
        {
            if (methodInfo.DeclaringType == null) return null;
            var dynamicMethod = new DynamicMethod(string.Empty, typeof(object),
                                                  new[] { typeof(object), typeof(object[]) },
                                                  methodInfo.DeclaringType.Module);
            var il = dynamicMethod.GetILGenerator();
            var ps = methodInfo.GetParameters();
            var paramTypes = new Type[ps.Length];
            for (int i = 0; i < paramTypes.Length; i++)
            {
                if (ps[i].ParameterType.IsByRef)
                    paramTypes[i] = ps[i].ParameterType.GetElementType();
                else
                    paramTypes[i] = ps[i].ParameterType;
            }
            var locals = new LocalBuilder[paramTypes.Length];

            for (int i = 0; i < paramTypes.Length; i++)
            {
                locals[i] = il.DeclareLocal(paramTypes[i], true);
            }
            for (int i = 0; i < paramTypes.Length; i++)
            {
                il.Emit(OpCodes.Ldarg_1);
                EmitFastInt(il, i);
                il.Emit(OpCodes.Ldelem_Ref);
                EmitCastToReference(il, paramTypes[i]);
                il.Emit(OpCodes.Stloc, locals[i]);
            }
            if (!methodInfo.IsStatic)
            {
                il.Emit(OpCodes.Ldarg_0);
            }
            for (int i = 0; i < paramTypes.Length; i++)
            {
                il.Emit(ps[i].ParameterType.IsByRef ? OpCodes.Ldloca_S : OpCodes.Ldloc, locals[i]);
            }
            il.EmitCall(methodInfo.IsStatic ? OpCodes.Call : OpCodes.Callvirt, methodInfo, null);
            if (methodInfo.ReturnType == typeof(void))
                il.Emit(OpCodes.Ldnull);
            else
                EmitBoxIfNeeded(il, methodInfo.ReturnType);

            for (int i = 0; i < paramTypes.Length; i++)
            {
                if (ps[i].ParameterType.IsByRef)
                {
                    il.Emit(OpCodes.Ldarg_1);
                    EmitFastInt(il, i);
                    il.Emit(OpCodes.Ldloc, locals[i]);
                    var localType = locals[i].LocalType;
                    if (localType != null && localType.IsValueType)
                        il.Emit(OpCodes.Box, locals[i].LocalType);
                    il.Emit(OpCodes.Stelem_Ref);
                }
            }
            il.Emit(OpCodes.Ret);
            var invoder = (FastInvokeHandler)dynamicMethod.CreateDelegate(typeof(FastInvokeHandler));
            return invoder;
        }

        private static void EmitCastToReference(ILGenerator il, Type type)
        {
            il.Emit(type.IsValueType ? OpCodes.Unbox_Any : OpCodes.Castclass, type);
        }

        private static void EmitBoxIfNeeded(ILGenerator il, Type type)
        {
            if (type.IsValueType)
            {
                il.Emit(OpCodes.Box, type);
            }
        }

        private static void EmitFastInt(ILGenerator il, int value)
        {
            switch (value)
            {
                case -1:
                    il.Emit(OpCodes.Ldc_I4_M1);
                    return;
                case 0:
                    il.Emit(OpCodes.Ldc_I4_0);
                    return;
                case 1:
                    il.Emit(OpCodes.Ldc_I4_1);
                    return;
                case 2:
                    il.Emit(OpCodes.Ldc_I4_2);
                    return;
                case 3:
                    il.Emit(OpCodes.Ldc_I4_3);
                    return;
                case 4:
                    il.Emit(OpCodes.Ldc_I4_4);
                    return;
                case 5:
                    il.Emit(OpCodes.Ldc_I4_5);
                    return;
                case 6:
                    il.Emit(OpCodes.Ldc_I4_6);
                    return;
                case 7:
                    il.Emit(OpCodes.Ldc_I4_7);
                    return;
                case 8:
                    il.Emit(OpCodes.Ldc_I4_8);
                    return;
            }

            if (value > -129 && value < 128)
            {
                il.Emit(OpCodes.Ldc_I4_S, (SByte)value);
            }
            else
            {
                il.Emit(OpCodes.Ldc_I4, value);
            }
        }
    }
}
