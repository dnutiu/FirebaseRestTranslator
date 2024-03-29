# FirebaseRestTranslator

[![CircleCI](https://circleci.com/gh/dnutiu/FirebaseRestTranslator.svg?style=svg)](https://circleci.com/gh/dnutiu/FirebaseRestTranslator)

This is a small helper class that can be used to translate C#
dictionaries into the Firebase REST format so they be posted
to Firebase directly.

See the tests for usage.

```c#
    [Fact]
    public void Test_FirebaseTranslator_BoolValue()
    {
        var data = new Dictionary<string, object>()
        {
            ["myKey"] = false
        };
        var expectedJson = "{\"name\":\"Test_FirebaseTranslator\",\"fields\":{\"myKey\":{\"booleanValue\":false}}}";
        var result = Translator.Translate("Test_FirebaseTranslator", data);
        var actualJson = JsonConvert.SerializeObject(result);
        Assert.Equal(expectedJson, actualJson);
    }
```

# Changelog

### 0.1.2

- Added support for uint, long types.