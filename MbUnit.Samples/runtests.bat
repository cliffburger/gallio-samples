::SET filter="/f:include Type:Log4NetSample"
::SET filter="/f:include Type:TestLogDemo"
SET filter="/f:include Type:FactoryDataTest"
gallio.echo bin/MbUnit.Samples.dll  /rt:Html /show-reports %filter%