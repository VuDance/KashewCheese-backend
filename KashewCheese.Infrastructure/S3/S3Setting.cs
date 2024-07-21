using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashewCheese.Infrastructure.S3
{
    public class S3Setting
    {
        public string BucketName {  get; set; }
        public string AccessKey { get; set; }
        public string SecretKey {  get; set; }
        public string Region {  get; set; }
    }
}
