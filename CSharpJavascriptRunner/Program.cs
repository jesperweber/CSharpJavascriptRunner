using JavaScriptEngineSwitcher.Core;
using JavaScriptEngineSwitcher.Jurassic;
using JavaScriptEngineSwitcher.V8;
using Newtonsoft.Json;

// Setup JsEngineSwitcher
var engineSwitcher = JsEngineSwitcher.Current;
engineSwitcher.EngineFactories
    .AddJurassic()
    .AddV8();

var engine = JsEngineSwitcher.Current.CreateEngine(JurassicJsEngine.EngineName);

// Included js resources
engine.ExecuteResource("CSharpJavascriptRunner.schema.js", typeof(Program).Assembly);
engine.ExecuteResource("CSharpJavascriptRunner.sourceEntity.js", typeof(Program).Assembly);

// Run js schema
engine.Execute($"schema = new Schema()");
engine.Execute($"triggers = schema.triggers()");
engine.Execute($"route = schema.route(sourceEntity)");
engine.Execute($"view = schema.properties(sourceEntity)");

// Generated triggers, route and view
var triggers = engine.Evaluate<string>("JSON.stringify(triggers)");
var route = engine.Evaluate<string>("JSON.stringify(route)");
var view = engine.Evaluate<string>("JSON.stringify(view)");

File.WriteAllLinesAsync(Directory.GetCurrentDirectory() + "\\..\\..\\..\\view.json", new []{ JsonConvert.SerializeObject(JsonConvert.DeserializeObject(view), Formatting.Indented) });

Console.WriteLine(JsonConvert.SerializeObject(JsonConvert.DeserializeObject(triggers), Formatting.Indented));
Console.WriteLine(JsonConvert.SerializeObject(JsonConvert.DeserializeObject(route), Formatting.Indented));
Console.WriteLine(JsonConvert.SerializeObject(JsonConvert.DeserializeObject(view), Formatting.Indented));

Console.ReadLine();