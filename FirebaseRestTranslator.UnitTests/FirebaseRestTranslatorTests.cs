using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace FirebaseRestTranslator.UnitTests
{
    enum Season
    {
        Spring,
        Summer,
        Autumn,
        Winter
    }

    
    public class FirebaseRestTranslatorTests
    {
        public FirebaseRestTranslatorTests(ITestOutputHelper testOutputHelper)
        {
        }

        private Dictionary<string, object> getBasicTelemetryData()
        {
            var basicTelemetryDict = new Dictionary<string, object>
            {
                ["source"] = "NucuCar.Sensors",
                ["timestamp"] = "2019-12-01T23:26:13.5537227+02:00"
            };
            var data = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>()
                {
                    ["sensor_state"] = 2,
                    ["cpu_temperature"] = 48.849998474121094,
                    ["_id"] = "CpuTemperature",
                },
                new Dictionary<string, object>()
                {
                    ["sensor_state"] = 2,
                    ["temperature"] = 32.65,
                    ["humidity"] = 100.0,
                    ["pressure"] = 62228.49,
                    ["voc"] = 0.0,
                    ["_id"] = "Bme680-Sensor"
                }
            };
            basicTelemetryDict["data"] = data;
            return basicTelemetryDict;
        }

        [Fact]
        public void Test_FirebaseTranslator_Parse()
        {
            var expectedJson =
                "{\"name\":\"Test\",\"fields\":{\"source\":{\"stringValue\":\"NucuCar.Sensors\"},\"timestamp\":{\"stringValue\":\"2019-12-01T23:26:13.5537227+02:00\"},\"data\":{\"arrayValue\":{\"values\":[{\"mapValue\":{\"fields\":{\"sensor_state\":{\"integerValue\":2},\"cpu_temperature\":{\"doubleValue\":48.849998474121094},\"_id\":{\"stringValue\":\"CpuTemperature\"}}}},{\"mapValue\":{\"fields\":{\"sensor_state\":{\"integerValue\":2},\"temperature\":{\"doubleValue\":32.65},\"humidity\":{\"doubleValue\":100.0},\"pressure\":{\"doubleValue\":62228.49},\"voc\":{\"doubleValue\":0.0},\"_id\":{\"stringValue\":\"Bme680-Sensor\"}}}}]}}}}";
            var basicTelemetryData = getBasicTelemetryData();
            var result = Translator.Translate("Test", basicTelemetryData);
            var json = JsonConvert.SerializeObject(result);
            Assert.Equal(expectedJson, json);
        }
        
        [Fact]
        public void Test_FirebaseTranslator_StringValue()
        {
            var data = new Dictionary<string, object>()
            {
                ["myKey"] = "myValue"
            };
            var expectedJson = "{\"name\":\"Test_FirebaseTranslator_StringValue\",\"fields\":{\"myKey\":{\"stringValue\":\"myValue\"}}}";
            var result = Translator.Translate("Test_FirebaseTranslator_StringValue", data);
            var actualJson = JsonConvert.SerializeObject(result);
            Assert.Equal(expectedJson, actualJson);
        }
        
        [Fact]
        public void Test_FirebaseTranslator_IntegerValue()
        {
            var data = new Dictionary<string, object>()
            {
                ["myKey"] = 23
            };
            var expectedJson = "{\"name\":\"Test_FirebaseTranslator\",\"fields\":{\"myKey\":{\"integerValue\":23}}}";
            var result = Translator.Translate("Test_FirebaseTranslator", data);
            var actualJson = JsonConvert.SerializeObject(result);
            Assert.Equal(expectedJson, actualJson);
        }
        
        [Fact]
        public void Test_FirebaseTranslator_IntegerValue_Uint()
        {
            var data = new Dictionary<string, object>()
            {
                ["myKey"] = (uint) 23
            };
            var expectedJson = "{\"name\":\"Test_FirebaseTranslator\",\"fields\":{\"myKey\":{\"integerValue\":23}}}";
            var result = Translator.Translate("Test_FirebaseTranslator", data);
            var actualJson = JsonConvert.SerializeObject(result);
            Assert.Equal(expectedJson, actualJson);
        }
        
        [Fact]
        public void Test_FirebaseTranslator_IntegerValue_Long()
        {
            var data = new Dictionary<string, object>()
            {
                ["myKey"] = (long) 23
            };
            var expectedJson = "{\"name\":\"Test_FirebaseTranslator\",\"fields\":{\"myKey\":{\"integerValue\":23}}}";
            var result = Translator.Translate("Test_FirebaseTranslator", data);
            var actualJson = JsonConvert.SerializeObject(result);
            Assert.Equal(expectedJson, actualJson);
        }

        [Fact]
        public void Test_FirebaseTranslator_DoubleValue()
        {
            var data = new Dictionary<string, object>()
            {
                ["myKey"] = 11.20
            };
            var expectedJson = "{\"name\":\"Test_FirebaseTranslator\",\"fields\":{\"myKey\":{\"doubleValue\":11.2}}}";
            var result = Translator.Translate("Test_FirebaseTranslator", data);
            var actualJson = JsonConvert.SerializeObject(result);
            Assert.Equal(expectedJson, actualJson);
        }
        
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
        
        [Fact]
        public void Test_FirebaseTranslator_TimestampValue()
        {
            var data = new Dictionary<string, object>()
            {
                ["myKey"] = new DateTime(2020, 2, 29, 0, 0, 0, 0)
            };
            var expectedJson = "{\"name\":\"Test_FirebaseTranslator\",\"fields\":{\"myKey\":{\"timestampValue\":\"2020-02-29T00:00:00\"}}}";
            var result = Translator.Translate("Test_FirebaseTranslator", data);
            var actualJson = JsonConvert.SerializeObject(result);
            Assert.Equal(expectedJson, actualJson);
        }
        
        [Fact]
        public void Test_FirebaseTranslator_EnumValue()
        {
            var data = new Dictionary<string, object>()
            {
                ["myKey"] = Season.Winter
            };
            var expectedJson = "{\"name\":\"Test_FirebaseTranslator\",\"fields\":{\"myKey\":{\"integerValue\":3}}}";
            var result = Translator.Translate("Test_FirebaseTranslator", data);
            var actualJson = JsonConvert.SerializeObject(result);
            Assert.Equal(expectedJson, actualJson);
        }
        
        [Fact]
        public void Test_FirebaseTranslator_ArrayValue()
        {
            var data = new Dictionary<string, object>()
            {
                ["myKey"] = new List<Dictionary<string, object>>
                {
                    new Dictionary<string, object>
                    {
                        ["arrayIndex0"] = 11.20
                    },
                    new Dictionary<string, object>
                    {
                        ["arrayIndex1"] = "test"
                    }
                }
            };
            var expectedJson = "{\"name\":\"Test_FirebaseTranslator\",\"fields\":{\"myKey\":{\"arrayValue\":{\"values\":[{\"mapValue\":{\"fields\":{\"arrayIndex0\":{\"doubleValue\":11.2}}}},{\"mapValue\":{\"fields\":{\"arrayIndex1\":{\"stringValue\":\"test\"}}}}]}}}}";
            var result = Translator.Translate("Test_FirebaseTranslator", data);
            var actualJson = JsonConvert.SerializeObject(result);
            Assert.Equal(expectedJson, actualJson);
        }
        
        [Fact]
        public void Test_FirebaseTranslator_MapValue()
        {
            var data = new Dictionary<string, object>()
            {
                ["myKey"] = new Dictionary<string, object>
                {
                    ["stringKey"] = "test",
                    ["boolKey"] = true,
                    ["intKey"] = 99
                }
            };
            var expectedJson = "{\"name\":\"Test_FirebaseTranslator\",\"fields\":{\"myKey\":{\"mapValue\":{\"fields\":{\"stringKey\":{\"stringValue\":\"test\"},\"boolKey\":{\"booleanValue\":true},\"intKey\":{\"integerValue\":99}}}}}}";
            var result = Translator.Translate("Test_FirebaseTranslator", data);
            var actualJson = JsonConvert.SerializeObject(result);
            Assert.Equal(expectedJson, actualJson);
        }
        
        [Fact]
        public void Test_FirebaseTranslator_NullValue()
        {
            var data = new Dictionary<string, object>()
            {
                ["myKey"] = null
            };
            var expectedJson = "{\"name\":\"Test_FirebaseTranslator\",\"fields\":{\"myKey\":{\"nullValue\":null}}}";
            var result = Translator.Translate("Test_FirebaseTranslator", data);
            var actualJson = JsonConvert.SerializeObject(result);
            Assert.Equal(expectedJson, actualJson);
        }
        
        [Fact]
        public void Test_FirebaseTranslator_BytesValue()
        {
            var data = new Dictionary<string, object>()
            {
                ["myKey"] = new byte[] {97, 98, 99, 100, 101, 102, 103, 104, 105 }
            };
            var expectedJson = "{\"name\":\"Test_FirebaseTranslator\",\"fields\":{\"myKey\":{\"bytesValue\":\"YWJjZGVmZ2hp\"}}}";
            var result = Translator.Translate("Test_FirebaseTranslator", data);
            var actualJson = JsonConvert.SerializeObject(result);
            Assert.Equal(expectedJson, actualJson);
        }
        
        [Fact]
        public void Test_FirebaseTranslator_ReferenceValue()
        {
            var data = new Dictionary<string, object>()
            {
                ["myKey"] = new ReferenceValue("test")
            };
            var expectedJson = "{\"name\":\"Test_FirebaseTranslator\",\"fields\":{\"myKey\":{\"referenceValue\":\"test\"}}}";
            var result = Translator.Translate("Test_FirebaseTranslator", data);
            var actualJson = JsonConvert.SerializeObject(result);
            Assert.Equal(expectedJson, actualJson);
        }
        
        [Fact]
        public void Test_FirebaseTranslator_GeoPointvalue()
        {
            var data = new Dictionary<string, object>()
            {
                ["myKey"] = new GeoPointValue(10, 22)
            };
            var expectedJson = "{\"name\":\"Test_FirebaseTranslator\",\"fields\":{\"myKey\":{\"geoPointValue\":{\"latitude\":10.0,\"longitude\":22.0}}}}";
            var result = Translator.Translate("Test_FirebaseTranslator", data);
            var actualJson = JsonConvert.SerializeObject(result);
            Assert.Equal(expectedJson, actualJson);
        }
    }
}