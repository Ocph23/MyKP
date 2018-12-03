'use strict';
angular.module('admin.routes', [])
    .config(function ($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('admin', {
                url: '/admin',
                templateUrl: '/apps/admin/index.html',
                controller: 'AdminUserController'
            })

            .state('dashboard', {
                url: '/dashboard',
                parent:'admin',
                templateUrl: '/apps/admin/index.html',
                controller: 'AdminUserController'
            })

            .state('adminagent', {
                url: '/agent',
                parent: 'admin',
                templateUrl: '/apps/admin/templates/agent.html',
                controller: 'AdminAgentController'
            })


            ;
           

    })
   ;