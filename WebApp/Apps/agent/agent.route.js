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
                params: {
                    data: null
                },
                templateUrl: '/apps/agent/templates/createmanifest.html',
                controller: 'AgentCreateManifestController'

            }).state('manifests', {
                url: '/manifests',
                parent: 'agent',
                templateUrl: '/apps/agent/templates/manifests.html',
                controller: 'AgentManifestController'
            })
            .state('agentfindstt', {
                url: '/agentfindstt',
                parent: 'agent',
                templateUrl: '/apps/agent/templates/agentfindstt.html',
                controller: 'AgentFindSTTController'
            })

            .state('agentinvoice', {
                url: '/agentinvoice',
                parent: 'agent',
                templateUrl: '/apps/agent/templates/agentinvoice.html',
                controller: 'AgentInvoiceController'
            })

            .state('agentprice', {
                url: '/agentprice',
                parent: 'agent',
                templateUrl: '/apps/agent/templates/agentprice.html',
                controller: 'AgentPriceController'
            })

            .state('agentdashboard', {
                url: '/agentdashboard',
                parent: 'agent',
                templateUrl: '/apps/agent/templates/agentdashboard.html',
                controller: 'AgentDashboardController'
            })

            ;


        
    });