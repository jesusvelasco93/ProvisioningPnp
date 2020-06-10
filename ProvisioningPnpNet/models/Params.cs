using System.Collections.Generic;

namespace ProvisioningPnpNet.models
{

    public struct BaseParamModel
    {
        public string value { get; set; }
        public string[] validations { get; set; }

        public BaseParamModel(string _value, string[] _validations)
        {
            this.value = _value;
            this.validations = _validations;
        }
    }


    public struct DefinedParamsModel
    {
        public BaseParamModel username { get; set; }
        public BaseParamModel password { get; set; }
        public BaseParamModel urlSite { get; set; }
        public BaseParamModel templateName { get; set; }
        public BaseParamModel dirTemplate { get; set; }
    }

    public struct ParamsModel
    {
        public DefinedParamsModel basicParams { get; set; }
        public Dictionary<string, BaseParamModel> others { get; set; }

        public ParamsModel(bool init) {
            this.basicParams = new DefinedParamsModel();
            this.others = new Dictionary<string, BaseParamModel> { };
        }
    }

    public struct ValidationParamsModel
    {
        public bool valid { get; set; }
        public string[] errors { get; set; }
        public ValidationParamsModel(bool _valid, string[] _errors)
        {
            this.valid = _valid;
            this.errors = _errors;
        }

    }
}
