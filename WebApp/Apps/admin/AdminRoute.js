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

            .state('usermanage', {
                url: '/usermanage',
                parent: 'admin',
                params: {
                    data: null
                },
                templateUrl: '/apps/admin/templates/manageuser.html',
                controller: 'AdminUserManageController'
            })

            .state('cities', {
                url: '/cities',
                parent: 'admin',
                params: {
                    data: null
                },
                templateUrl: '/apps/admin/templates/city.html',
                controller: 'AdminCityManageController'
            })

            .state('prices', {
                url: '/prices',
                parent: 'admin',
                params: {
                    data: null
                },
                templateUrl: '/apps/admin/templates/prices.html',
                controller: 'AdminPriceManageController'
            })
            ;
    })
   ;