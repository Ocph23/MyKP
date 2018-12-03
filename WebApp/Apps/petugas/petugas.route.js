//'use strict';
angular.module('petugas.routes', [])
    .config(function ($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('petugas', {
                url: '/petugas',
                templateUrl: '/apps/templates/petugas/index.html',
                controller: 'PetugasHomeController'
            })
        
    })
   ;