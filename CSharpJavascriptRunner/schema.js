Schema = class {
    triggers() {
        return {
            "umbraco": ["home"]
        }
    }

    route(sourceEntity) {
        return {
            "url": sourceEntity.url
        }
    }

    properties(sourceEntity) {

        var stringValue = null;
        if (sourceEntity.properties.nullString) {
            stringValue = sourceEntity.properties.nullString;
        }
        else {
            stringValue = sourceEntity.properties.string;
        }

        return {
            "string": stringValue,
            "string1": sourceEntity.properties.string,
            "number1": sourceEntity.properties.number,
            "boolean1": sourceEntity.properties.boolean,
            "test": "hej med dig og mig: " + sourceEntity.properties.string
        };
    }
}