using MonoMod.RuntimeDetour;
using System;
using System.Reflection;

namespace LawnMod
{
    public static class MonoModUtils
    {
        /* 
         * Purpose: process dynamic functions from scripting engine (ironpython etc.) and
         * turns it into a valid delegate for MonoMod.RuntimeDetour.
         */

        public static MethodBase GetFirstTargetAnyway(in dynamic o) 
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

        public static MethodBase GetFirstTargetFromPythonMethodDesc(in IronPython.Runtime.Types.BuiltinMethodDescriptor desc) 
        {
            return GetPrivateProperty<IronPython.Runtime.Types.BuiltinFunction>(desc, "Template").Targets[0];
        }

        public static MethodBase GetFirstTargetFromPythonBuiltinFunc(in IronPython.Runtime.Types.BuiltinFunction func)
        {
            return func.Targets[0];
        }

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

        public static dynamic/*Func<dynamic, Delegate>*/ HookTo(dynamic funcOrMethoddesc) 
        {
            throw new NotImplementedException();
        }

        public static Func<dynamic, Delegate> AsFunc(params Type[] generics)
        {
            Type[] rdgenerics = MakeRuntimeDetourGenerics(generics);
            return (hookerDynScriptFunc) =>
            {
                Type t_his = typeof(MonoModUtils);
                return (Delegate)t_his
                    .GetMethod("F", rdgenerics.Length, new Type[] { typeof(object) })
                    .MakeGenericMethod(rdgenerics)
                    .Invoke(t_his, new object[] { hookerDynScriptFunc });
            };
        }

        public static Func<dynamic, Delegate> AsAction(params Type[] generics)
        {
            Type[] generics2 = new Type[generics.Length + 1];
            generics.CopyTo(generics2, 0);
            generics2[^1] = typeof(void);
            Type[] rdgenerics = MakeRuntimeDetourGenerics(generics2);
            return (hookerDynScriptFunc) =>
            {
                Type t_his = typeof(MonoModUtils);
                return (Delegate)t_his
                    .GetMethod("A", rdgenerics.Length, new Type[] { typeof(object) })
                    .MakeGenericMethod(rdgenerics)
                    .Invoke(t_his, new object[] { hookerDynScriptFunc });
            };
        }

        public static Delegate AsDelegateFuncType(Type delegateType, dynamic hookerDynScriptFunc)
        {
            Type[] generics = delegateType.GetGenericArguments();
            Type t_his = typeof(MonoModUtils);
            return (Delegate)t_his
                .GetMethod("F", generics.Length, new Type[] { typeof(object) })
                .MakeGenericMethod(generics)
                .Invoke(t_his, new object[] { hookerDynScriptFunc });
        }

        public static Delegate AsDelegateActionType(Type[] generics, dynamic hookerDynScriptFunc)
        {
            Type t_his = typeof(MonoModUtils);
            return (Delegate)t_his
                .GetMethod("A", generics.Length, new Type[] { typeof(object) })
                .MakeGenericMethod(generics)
                .Invoke(t_his, new object[] { hookerDynScriptFunc });
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

        public static DynamicHookGen On = DynamicHookGen.On;

        public static DynamicHookGen OnOrIL = DynamicHookGen.OnOrIL;

        public static DynamicHookGen IL = DynamicHookGen.IL;
    }
}
