using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;

using Amazon;
using Amazon.Lambda.Core;
using Amazon.Lambda.DynamoDBEvents;
using Amazon.Lambda.TestUtilities;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

using VogCodeChallenge.Lambda;

namespace VogCodeChallenge.Lambda.Tests
{
    public class FunctionTest
    {
        [Fact]
        public void TestFunction()
        {
            DynamoDBEvent evnt = new DynamoDBEvent
            {
                Records = new List<DynamoDBEvent.DynamodbStreamRecord>
                {
                    new DynamoDBEvent.DynamodbStreamRecord
                    {
                        AwsRegion = "us-west-2",
                        Dynamodb = new StreamRecord
                        {
                            ApproximateCreationDateTime = DateTime.Now,
                            Keys = new Dictionary<string, AttributeValue> { {"Id", new AttributeValue { S = "35" } } },
                            NewImage = new Dictionary<string, AttributeValue> { { "FirstName", new AttributeValue { S = "Caio" } }, { "LastName", new AttributeValue { S = "Bulgarelli" } } },
                            OldImage = new Dictionary<string, AttributeValue> { { "FirstName", new AttributeValue { S = "Vog" } }, { "LastName", new AttributeValue { S = "Challenge" } } },
                            StreamViewType = StreamViewType.NEW_AND_OLD_IMAGES
                        }
                    }
                }
            };


            TestLambdaContext context = new TestLambdaContext();
            Function function = new Function();

            function.FunctionHandler(evnt, context);

            TestLambdaLogger testLogger = context.Logger as TestLambdaLogger;
			Assert.Contains("Record Id: 35", testLogger.Buffer.ToString());
			Assert.Contains("Stream processing complete", testLogger.Buffer.ToString());
        }  
    }
}
