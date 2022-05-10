using Jering.Javascript.NodeJS;

namespace DateParserExample
{
    internal class Program
    {
        static async Task Main(string[] _)
        {
            // Configure path where StaticNodeJSService will look for modules
            StaticNodeJSService.Configure<NodeJSProcessOptions>(options =>
            {
                options.ProjectPath = Path.Combine(Directory.GetCurrentDirectory(), "Javascript");
            });

            // Dummy module that imports date-format-parse (https://www.npmjs.com/package/date-format-parse)            
            const string dummyModule = @"
const dateFormatParse = require('date-format-parse');

module.exports = (callback) => {
    const date = dateFormatParse.parse('2019-12-10 14:11:12', 'YYYY-MM-DD HH:mm:ss');
    callback(null, date.getFullYear());
}";


            // Invoke module
            int resultYear = await StaticNodeJSService.InvokeFromStringAsync<int>(dummyModule);

            // Year should be 2019
            Console.WriteLine(resultYear);
        }
    }
}