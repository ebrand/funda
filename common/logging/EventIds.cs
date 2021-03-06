using System;

namespace funda.common.logging
{
    public static class Events
    {
        public static class Repository
        {
            public static class Create
            {
                public static int Success	 = 10;
                public static int Warning	 = 11;
                public static int Failure	 = 12;
				public static int InProgress = 13;
			}
            public static class Read
            {
                public static int Success	 = 20;
                public static int Warning	 = 21;
                public static int Failure	 = 22;
				public static int InProgress = 23;
			}
            public static class Update
            {
                public static int Success	 = 30;
                public static int Warning	 = 31;
                public static int Failure	 = 32;
				public static int InProgress = 33;
			}
            public static class Delete
            {
                public static int Success	 = 40;
                public static int Warning	 = 41;
                public static int Failure	 = 42;
				public static int InProgress = 43;
			}
			public static class Search
			{
				public static int Success = 50;
				public static int Warning = 51;
				public static int Failure = 52;
				public static int InProgress = 53;
			}
        }

        public static class ApiController
        {
            public static class Create
            {
                public static int Success	 = 100;
                public static int Warning	 = 101;
                public static int Failure	 = 102;
				public static int InProgress = 103;
			}
            public static class Read
            {
                public static int Success	 = 200;
                public static int Warning	 = 201;
                public static int Failure	 = 202;
				public static int InProgress = 203;
			}
            public static class Update
            {
                public static int Success	 = 300;
                public static int Warning	 = 301;
                public static int Failure	 = 302;
				public static int InProgress = 303;
			}
            public static class Delete
            {
                public static int Success	 = 400;
                public static int Warning	 = 401;
                public static int Failure	 = 402;
				public static int InProgress = 403;
			}
			public static class Search
			{
				public static int Success = 500;
				public static int Warning = 501;
				public static int Failure = 502;
				public static int InProgress = 503;
			}
        }

        public static class WebController
        {
            public static class Create
            {
                public static int Success	 = 1000;
                public static int Warning	 = 1001;
                public static int Failure	 = 1002;
				public static int InProgress = 1003;
			}
            public static class Read
            {
                public static int Success	 = 2000;
                public static int Warning	 = 2001;
                public static int Failure	 = 2002;
				public static int InProgress = 2003;
			}
            public static class Update
            {
                public static int Success	 = 3000;
                public static int Warning	 = 3001;
                public static int Failure	 = 3002;
				public static int InProgress = 3003;
			}
            public static class Delete
            {
                public static int Success	 = 4000;
                public static int Warning	 = 4001;
                public static int Failure	 = 4002;
				public static int InProgress = 4003;
			}
			public static class Search
			{
				public static int Success = 5000;
				public static int Warning = 5001;
				public static int Failure = 5002;
				public static int InProgress = 5003;
			}
        }
    }
}