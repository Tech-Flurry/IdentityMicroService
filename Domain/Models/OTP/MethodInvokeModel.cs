using System.Collections.Generic;

namespace Domain.Models.OTP
{
    public class MethodInvokeModel
    {
        public string MethodName { get; set; }
        public List<MethodParams> Params { get; set; }
    }
    public class MethodParams
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public string Type { get; set; }
        public bool IsInJson { get; set; }
    }
}
