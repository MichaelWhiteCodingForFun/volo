//
//  angular bootup and routing table
//


console.log("POD Bootstrap");

(function () {

    var app = angular.module('podApp', ['ngRoute', 'ui.bootstrap', 'ngSanitize', 'blockUI']);

    app.config(['$controllerProvider', '$provide', function ($controllerProvider, $provide) {
        app.register =
          {
              controller: $controllerProvider.register,
              service: $provide.service
          };
    }]);

})();

console.log("POD Bootstrap FINISHED 2");




