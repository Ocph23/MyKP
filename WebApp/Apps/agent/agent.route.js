//'use strict';
angular.module('agent.routes', [])
    .config(function ($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('agent', {
                url: '/agent',
                templateUrl: '/apps/agent/index.html',
                controller: 'AgentHomeController'

            }).state('createmanifest', {
                url: '/createmanifest',
                parent: 'agent',
                templateUrl: '/apps/agent/templates/createmanifest.html',
                controller: 'AgentCreateManifestController'

            }).state('manifests', {
                url: '/manifests',
                parent: 'agent',
                templateUrl: '/apps/agent/templates/manifests.html',
                controller: 'AgentManifestController'
            });
        
    });