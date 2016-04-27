
console.log("user inquiry controller load");

angular.module("podApp").register.controller('UserMaintenanceController', ['$routeParams', '$location', 'ajaxService', 'dataGridService', 'alertService',
    function ($routeParams, $location, ajaxService, dataGridService, alertService) {

        "use strict";

        var vm = this;

        this.initializeController = function () {

            vm.title = "User Inquiry";
            vm.users = [];
            vm.userData = {};
            vm.selected = {};
            vm.alerts = [];
            vm.closeAlert = alertService.closeAlert;

            dataGridService.initializeTableHeaders();

            dataGridService.addHeader("Payer Account #", "PayerAccountNumber");
            dataGridService.addHeader("Payer Account Name", "PayerAccountName");
            dataGridService.addHeader("Email", "Email");
            dataGridService.addHeader("Edit", "Edit");

            vm.tableHeaders = dataGridService.setTableHeaders();
            vm.defaultSort = dataGridService.setDefaultSort("Payer Account Name");

            vm.currentPageNumber = 1;
            vm.sortExpression = "PayerAccountName";
            vm.sortDirection = "ASC";
            vm.pageSize = 15;

            this.executeInquiry();

        }

        this.closeAlert = function (index) {
            vm.alerts.splice(index, 1);
        };

        this.changeSorting = function (column) {

            dataGridService.changeSorting(column, vm.defaultSort, vm.tableHeaders);

            vm.defaultSort = dataGridService.getSort();
            vm.sortDirection = dataGridService.getSortDirection();
            vm.sortExpression = dataGridService.getSortExpression();
            vm.currentPageNumber = 1;

            vm.executeInquiry();

        };

        this.setSortIndicator = function (column) {
            return dataGridService.setSortIndicator(column, vm.defaultSort);
        };

        this.pageChanged = function () {
            this.executeInquiry();
        }

        this.executeInquiry = function () {
            var inquiry = vm.prepareSearch();
            ajaxService.ajaxPost(inquiry, "api/CustomerService/GetCustomers", this.getUsersOnSuccess, this.getUsersOnError);
        }

        this.prepareSearch = function () {

            var inquiry = new Object();

            inquiry.currentPageNumber = vm.currentPageNumber;
            inquiry.sortExpression = vm.sortExpression;
            inquiry.sortDirection = vm.sortDirection;
            inquiry.pageSize = vm.pageSize;

            return inquiry;

        }

        this.getUsersOnSuccess = function (response) {
            vm.users = response.customers;
            vm.totalUsers = response.totalRows;
            vm.totalPages = response.totalPages;
        }

        this.getUsersOnError = function (response) {
            alertService.RenderErrorMessage(response.ReturnMessage);
        }

        this.getTemplate = function (user) {
            if (user.id === vm.selected.id) {
                return 'edit';
            }
            else return 'display';
        };

        this.editUser = function (user) {
            vm.selected = angular.copy(user);
        };

        this.reset = function () {
            vm.selected = {};
        };

        this.addUser = function () {
            vm.userData = {
                payerAccountNumber: vm.payerAccountNumber,
                payerAccountName: vm.payerAccountName,
                email: vm.email
            };
            vm.users.push(vm.userData);
            console.log(vm.users);
        };

        this.addUsertoDB = function (user) {
            ajaxService.ajaxPost(user, "api/UserService/addUsertoDB", function () {
                console.log("emp added");
                vm.getUsers();
            }, function () {
                console.log("emp adding failed");
            });

        };

        this.updateUser = function (user) {

            ajaxService.ajaxPost(user, "api/UserService/updateUser",
                function (data) {
                    console.log(data);
                    vm.reset();
                    vm.getUsers();
                },
                function () {
                    console.log("Error updating");
                }
            );

        };


        this.deleteUser = function (user) {

            ajaxService.ajaxPost(user, "api/UserService/deleteUser",
                function (data) {
                    console.log(data);
                    vm.getUsers();
                },
                function () {
                    console.log("Error deleting");
                }
            );
        };

        this.addOrUpdateUser = function (user) {
            if (user.id) {
                vm.updateUser(user);
            } else {
                vm.addUsertoDB(user);
            }

        }

        this.getUsers = function () {

            ajaxService.ajaxPost("api/UserService/getUser",
                function (data) {
                    vm.users = data;
                },
                function () {
                    console.log("Error adding to DB");
                }
            );
        };

    }]);
