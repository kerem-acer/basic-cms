namespace WebApi.Constants
{
    public static class ApiRoutes
    {
        public const string Base = "api/";

        public static class Pages
        {
            public const string GetAll = Base + "pages";
            public const string GetById = Base + "pages/{id:int}";
            public const string Create = Base + "pages";
            public const string Update = Base + "pages/{id:int}";
            public const string DeleteById = Base + "pages/{id:int}";
        }

        public static class Containers
        {
            public const string GetAll = Base + "containers";
            public const string GetById = Base + "containers/{id:int}";
            public const string GetContainersByPageId = Base + "pages/{pageId:int}/containers";
            public const string Create = Base + "containers";
            public const string Update = Base + "containers/{id:int}";
            public const string DeleteById = Base + "containers/{id:int}";
        }
    }
}