// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization.DataContracts;

namespace System.Runtime.Serialization.Json
{
    internal sealed class JsonEnumDataContract : JsonDataContract
    {
        private readonly JsonEnumDataContractCriticalHelper _helper;

        [RequiresUnreferencedCode(DataContract.SerializerTrimmerWarning)]
        public JsonEnumDataContract(EnumDataContract traditionalDataContract)
            : base(new JsonEnumDataContractCriticalHelper(traditionalDataContract))
        {
            _helper = (base.Helper as JsonEnumDataContractCriticalHelper)!;
        }

        public bool IsULong => _helper.IsULong;

        [RequiresUnreferencedCode(DataContract.SerializerTrimmerWarning)]
        public override object? ReadJsonValueCore(XmlReaderDelegator jsonReader, XmlObjectSerializerReadContextComplexJson? context)
        {
            object enumValue;
            if (IsULong)
            {
                enumValue = Enum.ToObject(TraditionalDataContract.UnderlyingType, jsonReader.ReadElementContentAsUnsignedLong());
            }
            else
            {
                enumValue = Enum.ToObject(TraditionalDataContract.UnderlyingType, jsonReader.ReadElementContentAsLong());
            }

            context?.AddNewObject(enumValue);
            return enumValue;
        }

        [RequiresUnreferencedCode(DataContract.SerializerTrimmerWarning)]
        public override void WriteJsonValueCore(XmlWriterDelegator jsonWriter, object obj, XmlObjectSerializerWriteContextComplexJson? context, RuntimeTypeHandle declaredTypeHandle)
        {
            if (IsULong)
            {
                jsonWriter.WriteUnsignedLong(Convert.ToUInt64(obj));
            }
            else
            {
                jsonWriter.WriteLong(Convert.ToInt64(obj));
            }
        }

        private sealed class JsonEnumDataContractCriticalHelper : JsonDataContractCriticalHelper
        {
            private readonly bool _isULong;

            [RequiresUnreferencedCode(DataContract.SerializerTrimmerWarning)]
            public JsonEnumDataContractCriticalHelper(EnumDataContract traditionalEnumDataContract)
                : base(traditionalEnumDataContract)
            {
                _isULong = traditionalEnumDataContract.IsULong;
            }

            public bool IsULong => _isULong;
        }
    }
}
