using Microsoft.CSharp.RuntimeBinder;
using MonoMod.RuntimeDetour;
using System;
using System.Dynamic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace LawnMod
{
    /// <summary>
    /// 各种辅助MonoMod以及使用IronPython进行动态Hook的工具，
    /// 例如将Python函数转为MonoMod.RuntimeDetour.DynamicHookGen可用的形式
    /// </summary>
    public static class MonoModUtils
    {
        /// <summary>
        /// 脱掉Python为函数方法加的壳，获取真正的MethodInfo
        /// </summary>
        /// <param name="o">输入的Python函数</param>
        /// <returns>函数内部的MethodInfo</returns>
        /// <exception cref="ArgumentException"></exception>
        public static MethodBase StripPythonMethodDesc(in dynamic o) 
        {
            Type t = o.GetType();
            if (t == typeof(IronPython.Runtime.Types.BuiltinMethodDescriptor))
            {
                return GetFirstTargetFromPythonMethodDesc(o as IronPython.Runtime.Types.BuiltinMethodDescriptor);
            }
            else if (t == typeof(IronPython.Runtime.Types.BuiltinFunction))
            {
                return GetFirstTargetFromPythonBuiltinFunc(o as IronPython.Runtime.Types.BuiltinFunction);
            }
            else 
            {
                throw new ArgumentException($"Invalid type of Argument:{t}");
            }
        }

        /// <summary>
        /// 获取Python方法描述符的第一个执行目标
        /// </summary>
        /// <param name="desc">Python方法描述</param>
        /// <returns>被封装的MethodInfo</returns>
        public static MethodBase GetFirstTargetFromPythonMethodDesc(in IronPython.Runtime.Types.BuiltinMethodDescriptor desc) 
        {
            return DynamicHelper.GetPrivateProperty<IronPython.Runtime.Types.BuiltinFunction>(desc, "Template").Targets[0];
        }

        /// <summary>
        /// 获取Python内置方法封装的第一个执行目标
        /// </summary>
        /// <param name="desc">Python内置方法</param>
        /// <returns>被封装的MethodInfo</returns>
        public static MethodBase GetFirstTargetFromPythonBuiltinFunc(in IronPython.Runtime.Types.BuiltinFunction func)
        {
            return func.Targets[0];
        }

        /// <summary>
        /// 获取方法的所有参数类型
        /// </summary>
        /// <param name="method">方法信息</param>
        /// <returns>参数类型数组</returns>
        public static Type[] GetMethodArgs(in MethodInfo method) 
        {
            var aParams = method.GetParameters();
            Type[] types = new Type[aParams.Length];
            for (int i = 0; i < aParams.Length; i++) 
            {
                types[i] = aParams[i].ParameterType;
            }
            return types;
        }

        /// <summary>
        /// 获取Func的类型泛型
        /// </summary>
        /// <param name="method">对应的方法</param>
        /// <returns>方法的类型泛型</returns>
        public static Type[] GetGenericsForFunc(in MethodInfo method)
        {
            var args = GetMethodArgs(method);
            Type[] types;
            if (!method.IsStatic)
            {
                types = new Type[args.Length + 2];
                args.CopyTo(types, 1);
                types[0] = method.DeclaringType;
            }
            else
            {
                types = new Type[args.Length + 1];
                args.CopyTo(types, 0);
            }
            types[types.Length-1] = method.ReturnType;
            return types;
        }

        /// <summary>
        /// 获取Action的类型泛型
        /// </summary>
        /// <param name="method">对应的方法</param>
        /// <returns>方法的类型泛型</returns>
        public static Type[] GetGenericsForAction(in MethodInfo method)
        {
            var args = GetMethodArgs(method);
            Type[] types;
            if (!method.IsStatic)
            {
                types = new Type[args.Length + 1];
                args.CopyTo(types, 1);
                types[0] = method.DeclaringType;
            }
            else
            {
                return args;
            }
            return types;
        }

        /// <summary>
        /// 制造适用于RuntimeDetour中DynamicHookGen的Target的泛型类型
        /// </summary>
        /// <param name="theTypes">函数类型</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static Type[] MakeRuntimeDetourGenerics(in Type[] theTypes) 
        {
            Type[] aTypes;
            bool doReturn = true;
            if (theTypes[^1] == typeof(void))
            {
                doReturn = false;
                aTypes = theTypes[..^1];
            }
            else 
            {
                aTypes = theTypes;
            }
                
            Type[] generics = new Type[aTypes.Length+1];
            Type t;
            if (doReturn)
            {
                switch (aTypes.Length)
                {
                    case 1:
                        t = typeof(Func<>); break;
                    case 2:
                        t = typeof(Func<,>); break;
                    case 3:
                        t = typeof(Func<,,>); break;
                    case 4:
                        t = typeof(Func<,,,>); break;
                    case 5:
                        t = typeof(Func<,,,,>); break;
                    case 6:
                        t = typeof(Func<,,,,,>); break;
                    case 7:
                        t = typeof(Func<,,,,,,>); break;
                    case 8:
                        t = typeof(Func<,,,,,,,>); break;
                    case 9:
                        t = typeof(Func<,,,,,,,,>); break;
                    case 10:
                        t = typeof(Func<,,,,,,,,,>); break;
                    case 11:
                        t = typeof(Func<,,,,,,,,,,>); break;
                    case 12:
                        t = typeof(Func<,,,,,,,,,,,>); break;
                    case 13:
                        t = typeof(Func<,,,,,,,,,,,,>); break;
                    case 14:
                        t = typeof(Func<,,,,,,,,,,,,,>); break;
                    case 15:
                        t = typeof(Func<,,,,,,,,,,,,,,>); break;
                    case 16:
                        t = typeof(Func<,,,,,,,,,,,,,,,>); break;
                    case 17:
                        t = typeof(Func<,,,,,,,,,,,,,,,,>); break;
                    default:
                        throw new NotImplementedException();
                }
                generics[0] = t.MakeGenericType(aTypes);
            }
            else
            {
                switch (aTypes.Length)
                {
                    case 0:
                        t = typeof(Action); break;
                    case 1:
                        t = typeof(Action<>); break;
                    case 2:
                        t = typeof(Action<,>); break;
                    case 3:
                        t = typeof(Action<,,>); break;
                    case 4:
                        t = typeof(Action<,,,>); break;
                    case 5:
                        t = typeof(Action<,,,,>); break;
                    case 6:
                        t = typeof(Action<,,,,,>); break;
                    case 7:
                        t = typeof(Action<,,,,,,>); break;
                    case 8:
                        t = typeof(Action<,,,,,,,>); break;
                    case 9:
                        t = typeof(Action<,,,,,,,,>); break;
                    case 10:
                        t = typeof(Action<,,,,,,,,,>); break;
                    case 11:
                        t = typeof(Action<,,,,,,,,,,>); break;
                    case 12:
                        t = typeof(Action<,,,,,,,,,,,>); break;
                    case 13:
                        t = typeof(Action<,,,,,,,,,,,,>); break;
                    case 14:
                        t = typeof(Action<,,,,,,,,,,,,,>); break;
                    case 15:
                        t = typeof(Action<,,,,,,,,,,,,,,>); break;
                    case 16:
                        t = typeof(Action<,,,,,,,,,,,,,,,>); break;
                    default: 
                        throw new NotImplementedException();
                }
                generics[0] = t.MakeGenericType(aTypes);
            }
            aTypes.CopyTo(generics, 1);
            return generics;
        }

        /// <summary>
        /// Python装饰器，为Python函数Hook一个特定的C#方法
        /// </summary>
        /// <param name="funcOrMethoddesc">要Hook的C#方法的Python封装</param>
        /// <param name="hookType">Hook的类型</param>
        /// <returns>一个装饰器，其接受Python函数，返回一个HookResult对象，通过此掌控Hook的生命周期</returns>
        public static Func<dynamic, HookResult> HookTo(dynamic funcOrMethoddesc, DynamicHookGen.HookType hookType = DynamicHookGen.HookType.OnOrIL)
        {
            Func<dynamic, Delegate> f = As(funcOrMethoddesc);
            MethodInfo method = (MethodInfo)StripPythonMethodDesc(in funcOrMethoddesc);
            return (hookerDynScriptFunc) =>
            {
                Delegate hookDelegate = f(hookerDynScriptFunc);
                return new HookResult(method, hookDelegate, hookType);
            };
        }

        /// <summary>
        /// Python装饰器，将Python函数转换为Hook一个C#方法所需的Delegate类型
        /// </summary>
        /// <param name="funcOrMethoddesc">要Hook的C#方法的Python封装</param>
        /// <returns>一个装饰器，其接受Python函数，返回所需的Delegate类型</returns>
        public static Func<dynamic, Delegate> As(dynamic funcOrMethoddesc) 
        {
            MethodInfo method = (MethodInfo)StripPythonMethodDesc(in funcOrMethoddesc);
            Func<dynamic, Delegate> f;
            if (method.ReturnType == typeof(void))
            {
                f = AsAction(GetGenericsForAction(in method));
            }
            else 
            {
                f = AsFunc(GetGenericsForFunc(in method));
            }
            return (hookerDynScriptFunc) =>
            {
                Delegate hookDelegate = f(hookerDynScriptFunc);
                return hookDelegate;
            };
        }


        /// <summary>
        /// Python装饰器，将Python函数转换为Hook一个有特定原型的C#有返回值方法所需的Delegate类型
        /// </summary>
        /// <param name="generics">C# Func原型的所有参数类型</param>
        /// <returns>一个装饰器，其接受Python函数，返回所需的Delegate类型</returns>
        public static Func<dynamic, Delegate> AsFunc(params Type[] generics)
        {
            Type[] rdgenerics = MakeRuntimeDetourGenerics(generics);
            return (hookerDynScriptFunc) =>
            {
                Type helper = typeof(MethodToDelegateHelper);
                return (Delegate)helper
                    .GetMethod("F", rdgenerics.Length, new Type[] { typeof(object) })
                    .MakeGenericMethod(rdgenerics)
                    .Invoke(helper, new object[] { hookerDynScriptFunc });
            };
        }

        /// <summary>
        /// Python装饰器，将Python函数转换为Hook一个有特定原型的C#无返回值方法所需的Delegate类型
        /// </summary>
        /// <param name="generics">C# Action原型的所有参数类型</param>
        /// <returns>一个装饰器，其接受Python函数，返回所需的Delegate类型</returns>
        public static Func<dynamic, Delegate> AsAction(params Type[] generics)
        {
            Type[] generics2 = new Type[generics.Length + 1];
            generics.CopyTo(generics2, 0);
            generics2[^1] = typeof(void);
            Type[] rdgenerics = MakeRuntimeDetourGenerics(generics2);
            return (hookerDynScriptFunc) =>
            {
                Type helper = typeof(MethodToDelegateHelper);
                return (Delegate)helper
                    .GetMethod("A", rdgenerics.Length, new Type[] { typeof(object) })
                    .MakeGenericMethod(rdgenerics)
                    .Invoke(helper, new object[] { hookerDynScriptFunc });
            };
        }

        public static DynamicHookGen On = DynamicHookGen.On;

        public static DynamicHookGen OnOrIL = DynamicHookGen.OnOrIL;

        public static DynamicHookGen IL = DynamicHookGen.IL;

        /// <summary>
        /// HookResult对象负责抽象创建Hook部分以及掌管Hook后事务
        /// </summary>
        public class HookResult : DynamicObject
        {
            /// <summary>
            /// 构造方法
            /// </summary>
            /// <param name="method">需要Hook的方法</param>
            /// <param name="hookDelegate">处理Hook的函数，需符合要求</param>
            /// <param name="hookType">Hook类型(On、IL、OnOrIL)</param>
            /// <param name="initialize">是否需要初始化(创建Hook)</param>
            internal HookResult(MethodInfo method, Delegate hookDelegate, DynamicHookGen.HookType hookType, bool initialize = true)
            {
                mParentalNode = new DynamicHookGen(method.DeclaringType, hookType);
                mMethodName = method.Name;
                mHookDelegate = hookDelegate;
                if (initialize)
                    Initialize();
            }

            /// <summary>
            /// Hook初始化
            /// </summary>
            private void Initialize()
            {
                DynamicHelper.SetDynamicMember(mParentalNode, mMethodName, (DynamicHookGen)DynamicHelper.GetDynamicMember(mParentalNode, mMethodName) + mHookDelegate);
            }

            /// <summary>
            /// 使HookResult类表现为一个可被调用的方法，转发参数
            /// </summary>
            /// <param name="binder"></param>
            /// <param name="args">调用方法的参数</param>
            /// <param name="result">返回值</param>
            /// <returns></returns>
            public override bool TryInvoke(InvokeBinder binder, object[] args, out object result)
            {
                result = mHookDelegate.DynamicInvoke(args);
                return true;
            }

            /// <summary>
            /// 该类析构时自动执行解除Hook
            /// </summary>
            ~HookResult()
            {
                UnHook();
            }

            /// <summary>
            /// 解除这个Hook
            /// </summary>
            public void UnHook()
            {
                DynamicHelper.SetDynamicMember(mParentalNode, mMethodName, (DynamicHookGen)DynamicHelper.GetDynamicMember(mParentalNode, mMethodName) - mHookDelegate);
            }

            readonly Delegate mHookDelegate;
            readonly string mMethodName;
            readonly DynamicHookGen mParentalNode;
        }

        public static class MethodToDelegateHelper
        {
            public static Delegate F<TResult>(dynamic theInputMethod)
            {
                return new Func<TResult>((Func<TResult>)theInputMethod);
            }

            public static Delegate F<T1, TResult>(dynamic theInputMethod)
            {
                return new Func<T1, TResult>((Func<T1, TResult>)theInputMethod);
            }

            public static Delegate F<T1, T2, TResult>(dynamic theInputMethod)
            {
                return new Func<T1, T2, TResult>((Func<T1, T2, TResult>)theInputMethod);
            }

            public static Delegate F<T1, T2, T3, TResult>(dynamic theInputMethod)
            {
                return new Func<T1, T2, T3, TResult>((Func<T1, T2, T3, TResult>)theInputMethod);
            }

            public static Delegate F<T1, T2, T3, T4, TResult>(dynamic theInputMethod)
            {
                return new Func<T1, T2, T3, T4, TResult>((Func<T1, T2, T3, T4, TResult>)theInputMethod);
            }

            public static Delegate F<T1, T2, T3, T4, T5, TResult>(dynamic theInputMethod)
            {
                return new Func<T1, T2, T3, T4, T5, TResult>((Func<T1, T2, T3, T4, T5, TResult>)theInputMethod);
            }

            public static Delegate F<T1, T2, T3, T4, T5, T6, TResult>(dynamic theInputMethod)
            {
                return new Func<T1, T2, T3, T4, T5, T6, TResult>((Func<T1, T2, T3, T4, T5, T6, TResult>)theInputMethod);
            }

            public static Delegate F<T1, T2, T3, T4, T5, T6, T7, TResult>(dynamic theInputMethod)
            {
                return new Func<T1, T2, T3, T4, T5, T6, T7, TResult>((Func<T1, T2, T3, T4, T5, T6, T7, TResult>)theInputMethod);
            }

            public static Delegate F<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(dynamic theInputMethod)
            {
                return new Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>((Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>)theInputMethod);
            }

            public static Delegate F<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(dynamic theInputMethod)
            {
                return new Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>((Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>)theInputMethod);
            }

            public static Delegate F<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(dynamic theInputMethod)
            {
                return new Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>((Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>)theInputMethod);
            }

            public static Delegate F<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(dynamic theInputMethod)
            {
                return new Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>((Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>)theInputMethod);
            }

            public static Delegate F<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(dynamic theInputMethod)
            {
                return new Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>((Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>)theInputMethod);
            }

            public static Delegate F<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(dynamic theInputMethod)
            {
                return new Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>((Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>)theInputMethod);
            }

            public static Delegate F<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(dynamic theInputMethod)
            {
                return new Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>((Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>)theInputMethod);
            }

            public static Delegate F<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(dynamic theInputMethod)
            {
                return new Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>((Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>)theInputMethod);
            }

            public static Delegate F<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(dynamic theInputMethod)
            {
                return new Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>((Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>)theInputMethod);
            }

            public static Delegate A<T1>(dynamic theInputMethod)
            {
                return new Action<T1>((Action<T1>)theInputMethod);
            }

            public static Delegate A<T1, T2>(dynamic theInputMethod)
            {
                return new Action<T1, T2>((Action<T1, T2>)theInputMethod);
            }

            public static Delegate A<T1, T2, T3>(dynamic theInputMethod)
            {
                return new Action<T1, T2, T3>((Action<T1, T2, T3>)theInputMethod);
            }

            public static Delegate A<T1, T2, T3, T4>(dynamic theInputMethod)
            {
                return new Action<T1, T2, T3, T4>((Action<T1, T2, T3, T4>)theInputMethod);
            }

            public static Delegate A<T1, T2, T3, T4, T5>(dynamic theInputMethod)
            {
                return new Action<T1, T2, T3, T4, T5>((Action<T1, T2, T3, T4, T5>)theInputMethod);
            }

            public static Delegate A<T1, T2, T3, T4, T5, T6>(dynamic theInputMethod)
            {
                return new Action<T1, T2, T3, T4, T5, T6>((Action<T1, T2, T3, T4, T5, T6>)theInputMethod);
            }

            public static Delegate A<T1, T2, T3, T4, T5, T6, T7>(dynamic theInputMethod)
            {
                return new Action<T1, T2, T3, T4, T5, T6, T7>((Action<T1, T2, T3, T4, T5, T6, T7>)theInputMethod);
            }

            public static Delegate A<T1, T2, T3, T4, T5, T6, T7, T8>(dynamic theInputMethod)
            {
                return new Action<T1, T2, T3, T4, T5, T6, T7, T8>((Action<T1, T2, T3, T4, T5, T6, T7, T8>)theInputMethod);
            }

            public static Delegate A<T1, T2, T3, T4, T5, T6, T7, T8, T9>(dynamic theInputMethod)
            {
                return new Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>((Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>)theInputMethod);
            }

            public static Delegate A<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(dynamic theInputMethod)
            {
                return new Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>((Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>)theInputMethod);
            }

            public static Delegate A<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(dynamic theInputMethod)
            {
                return new Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>((Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>)theInputMethod);
            }

            public static Delegate A<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(dynamic theInputMethod)
            {
                return new Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>((Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>)theInputMethod);
            }

            public static Delegate A<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(dynamic theInputMethod)
            {
                return new Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>((Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>)theInputMethod);
            }

            public static Delegate A<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(dynamic theInputMethod)
            {
                return new Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>((Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>)theInputMethod);
            }

            public static Delegate A<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(dynamic theInputMethod)
            {
                return new Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>((Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>)theInputMethod);
            }

            public static Delegate A<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(dynamic theInputMethod)
            {
                return new Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>((Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>)theInputMethod);
            }
        }
    }

    public static class DynamicHelper
    {
        public static object GetDynamicMember(object obj, string memberName)
        {
            var binder = Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, memberName, obj.GetType(),
                new[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) });
            var callsite = CallSite<Func<CallSite, object, object>>.Create(binder);
            return callsite.Target(callsite, obj);
        }

        public static void SetDynamicMember(object obj, string memberName, object value)
        {
            var binder = Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, memberName, obj.GetType(),
                new[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
                        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) });
            var callsite = CallSite<Func<CallSite, object, object, object>>.Create(binder);
            callsite.Target(callsite, obj, value);
        }

        public static T GetPrivateField<T>(this object instance, string fieldname)
        {
            BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic;
            Type type = instance.GetType();
            FieldInfo field = type.GetField(fieldname, flag);
            return (T)field.GetValue(instance);
        }

        public static T GetPrivateProperty<T>(this object instance, string propertyname)
        {
            BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic;
            Type type = instance.GetType();
            PropertyInfo field = type.GetProperty(propertyname, flag);
            return (T)field.GetValue(instance, null);
        }

        public static void SetPrivateField(this object instance, string fieldname, object value)
        {
            BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic;
            Type type = instance.GetType();
            FieldInfo field = type.GetField(fieldname, flag);
            field.SetValue(instance, value);
        }

        public static void SetPrivateProperty(this object instance, string propertyname, object value)
        {
            BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic;
            Type type = instance.GetType();
            PropertyInfo field = type.GetProperty(propertyname, flag);
            field.SetValue(instance, value, null);
        }

        public static T CallPrivateMethod<T>(this object instance, string name, params object[] param)
        {
            BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic;
            Type type = instance.GetType();
            MethodInfo method = type.GetMethod(name, flag);
            return (T)method.Invoke(instance, param);
        }

        public static T GetPrivateFieldStatic<T>(Type type, string fieldname)
        {
            BindingFlags flag = BindingFlags.Static | BindingFlags.NonPublic;
            FieldInfo field = type.GetField(fieldname, flag);
            return (T)field.GetValue(null);
        }

        public static T GetPrivatePropertyStatic<T>(Type type, string propertyname)
        {
            BindingFlags flag = BindingFlags.Static | BindingFlags.NonPublic;
            PropertyInfo field = type.GetProperty(propertyname, flag);
            return (T)field.GetValue(null, null);
        }

        public static void SetPrivateFieldStatic(Type type, string fieldname, object value)
        {
            BindingFlags flag = BindingFlags.Static | BindingFlags.NonPublic;
            FieldInfo field = type.GetField(fieldname, flag);
            field.SetValue(null, value);
        }

        public static void SetPrivatePropertyStatic(Type type, string propertyname, object value)
        {
            BindingFlags flag = BindingFlags.Static | BindingFlags.NonPublic;
            PropertyInfo field = type.GetProperty(propertyname, flag);
            field.SetValue(null, value, null);
        }

        public static T CallPrivateMethodStatic<T>(Type type, string name, params object[] param)
        {
            BindingFlags flag = BindingFlags.Static | BindingFlags.NonPublic;
            MethodInfo method = type.GetMethod(name, flag);
            return (T)method.Invoke(null, param);
        }
    }
}
