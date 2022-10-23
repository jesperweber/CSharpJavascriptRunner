using JavaScriptEngineSwitcher.Core;
using JavaScriptEngineSwitcher.Jurassic;
using JavaScriptEngineSwitcher.V8;

// Source entity
const string sourceEntity = @"
{
    ""url"": ""https://localhost/"",
    ""properties"": {
        ""nullString"": null,
        ""string"": ""value1"",
        ""number"": 1.5,
        ""boolean"": true
    }
}";

// Schema
const string schema = @"
    Schema = class {
        triggers() {
            return {
                ""umbraco"": [""home""]
            }
        }

        route(sourceEntity) {
            return {
                ""url"": sourceEntity.url
            }
        }

        properties(sourceEntity) {
    
            let stringValue= null;
            if(sourceEntity.properties.nullString){
                stringValue = sourceEntity.properties.nullString
            }
            else {
                stringValue = sourceEntity.properties.string
            }

            return {
                ""string"": stringValue,
                ""string1"": sourceEntity.properties.string,
                ""number1"": sourceEntity.properties.number,
                ""boolean1"": sourceEntity.properties.boolean,
                ""test"": ""hej med dig: "" + sourceEntity.properties.string
            };
        }
    }
";

// Setup JsEngineSwitcher
var engineSwitcher = JsEngineSwitcher.Current;
engineSwitcher.EngineFactories
    .AddJurassic()
    .AddV8();

var engine = JsEngineSwitcher.Current.CreateEngine(JurassicJsEngine.EngineName);

// Run js schema
engine.SetVariableValue("sourceEntity", sourceEntity);

engine.Execute(schema);
engine.Execute($"schema = new Schema()");
engine.Execute($"triggers = schema.triggers()");
engine.Execute($"route = schema.route(JSON.parse(sourceEntity))");
engine.Execute($"view = schema.properties(JSON.parse(sourceEntity))");

// Generated triggers, route and view
var triggers = engine.Evaluate<string>("JSON.stringify(triggers)");
var route = engine.Evaluate<string>("JSON.stringify(route)");
var view = engine.Evaluate<string>("JSON.stringify(view)");

Console.WriteLine(triggers);
Console.WriteLine(route);
Console.WriteLine(view);