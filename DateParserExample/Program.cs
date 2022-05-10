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

module.exports = (callback, rawDate) => {
    const date = dateFormatParse.parse(rawDate, 'YYYY-MM-DD HH:mm:ss');
    callback(null, date.getFullYear());
}";

            // Invoke module
            int resultYear = await StaticNodeJSService.InvokeFromStringAsync<int>(dummyModule, args: new string[] { "2019-12-10 14:11:12" });

            // Year should be 2019
            Console.WriteLine(resultYear);
        }
    }
}