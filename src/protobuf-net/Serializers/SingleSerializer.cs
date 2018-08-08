﻿#if !NO_RUNTIME
using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
    sealed class SingleSerializer : IProtoSerializer
    {
        static readonly Type expectedType = typeof(float);

        public Type ExpectedType { get { return expectedType; } }

        bool IProtoSerializer.RequiresOldValue => false;

        bool IProtoSerializer.ReturnsValue => true;

        public object Read(ProtoReader source, ref ProtoReader.State state, object value)
        {
            Helpers.DebugAssert(value == null); // since replaces
            return source.ReadSingle(ref state);
        }

        public void Write(object value, ProtoWriter dest)
        {
            ProtoWriter.WriteSingle((float)value, dest);
        }


#if FEAT_COMPILER
        void IProtoSerializer.EmitWrite(Compiler.CompilerContext ctx, Compiler.Local valueFrom)
        {
            ctx.EmitBasicWrite("WriteSingle", valueFrom);
        }
        void IProtoSerializer.EmitRead(Compiler.CompilerContext ctx, Compiler.Local valueFrom)
        {
            ctx.EmitBasicRead("ReadSingle", ExpectedType);
        }
#endif
    }
}
#endif