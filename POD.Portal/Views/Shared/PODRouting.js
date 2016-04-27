
console.log("alert PODRouting");

angular.module("podApp").config([
    '$routeProvider', '$locationProvider', 'applicationConfigurationProvider',
    function ($routeProvider, $locationProvider, applicationConfigurationProvider) {
        var self = this;
        this.getApplicationVersion = function () {
            var applicationVersion = applicationConfigurationProvider.getVersion();
            return applicationVersion;
        }

        var baseSiteUrlPath = $("base").first().attr("href");

        $routeProvider.when('/:section',
        {
            templateUrl: function (rp) {
                return baseSiteUrlPath + 'views/' + rp.section + '/' + 'index.html?v=' + self.getApplicationVersion();
            },

            resolve: {
                load: [
                    '$q', '$rootScope', '$location', function ($q, $rootScope, $location) {

                        var path = $location.path().split("/");
                        var directory = path[1];
                        var controllerName = "index";

                        var controllerToLoad = "Views/" + directory + "/" + controllerName + "Controller.js?v=" + self.getApplicationVersion();

                        var deferred = $q.defer();
                        require([controllerToLoad], function (err) {
                            $rootScope.$apply(function () {
                                deferred.resolve();
                            });
                        }, function (err) {
                            if (err) {
                                deferred.reject();
                                $location.path("home/notfound");
                            }
                        });

                        return deferred.promise;

                    }
                ]
            }
        });

        $routeProvider.when('/:section/:tree',
        {
            templateUrl: function (rp) { return baseSiteUrlPath + 'views/' + rp.section + '/' + rp.tree + '.html?v=' + self.getApplicationVersion(); },

            resolve: {
                load: [
                    '$q', '$rootScope', '$location', function ($q, $rootScope, $location) {

                        var path = $location.path().split("/");
                        var directory = path[1];
                        var controllerName = path[2];

                        var controllerToLoad = "Views/" + directory + "/" + controllerName + "Controller.js?v=" + self.getApplicationVersion();

                        var deferred = $q.defer();
                        require([controllerToLoad], function (err) {
                            $rootScope.$apply(function () {
                                deferred.resolve();
                            });
                        }, function (err) {
                            if (err) {
                                deferred.reject();
                                $location.path("home/notfound");
                            }
                        });

                        return deferred.promise;

                    }
                ]
            }


        });

        $routeProvider.when('/:section/:tree/:id',
        {
            templateUrl: function (rp) { return baseSiteUrlPath + 'views/' + rp.section + '/' + rp.tree + '.html?v=' + self.getApplicationVersion(); },

            resolve: {
                load: [
                    '$q', '$rootScope', '$location', function ($q, $rootScope, $location) {

                        var path = $location.path().split("/");
                        var directory = path[1];
                        var controllerName = path[2];

                        var controllerToLoad = "Views/" + directory + "/" + controllerName + "Controller.js?v=" + self.getApplicationVersion();

                        var deferred = $q.defer();
                        require([controllerToLoad], function () {
                            $rootScope.$apply(function () {
                                deferred.resolve();
                            });
                        }, function (err) {
                            if (err) {
                                deferred.reject();
                                $location.path("home/notfound");
                            }
                        });

                        return deferred.promise;

                    }
                ]
            }

        });

        $routeProvider.when('/',
        {
            templateUrl: function (rp) { return baseSiteUrlPath + 'views/home/Index.html?v=' + self.getApplicationVersion(); },

            resolve: {
                load: [
                    '$q', '$rootScope', '$location', function ($q, $rootScope, $location) {

                        var controllerToLoad = "Views/Home/IndexController.js?v=" + self.getApplicationVersion();

                        var deferred = $q.defer();
                        require([controllerToLoad], function () {
                            $rootScope.$apply(function () {
                                deferred.resolve();
                            });
                        }, function (err) {
                            if (err) {
                                deferred.reject();
                                $location.path("home/notfound");
                            }
                        });

                        return deferred.promise;

                    }
                ]
            }


        });

        $routeProvider.otherwise({
            redirectTo: "home/notfound"
        });

        $locationProvider.html5Mode(true);

    }
]);