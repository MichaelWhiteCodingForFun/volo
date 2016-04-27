
console.log("customer inquiry controller load");

angular.module("podApp").register.controller('CustomerMaintenanceController', ['$routeParams', '$location', 'ajaxService', 'dataGridService', 'alertService',
    function ($routeParams, $location, ajaxService, dataGridService, alertService) {

        "use strict";

        var vm = this;

        this.initializeController = function () {

            vm.title = "Customer Inquiry";
            vm.customers = [];
            vm.customerData = {};
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
            ajaxService.ajaxPost(inquiry, "api/CustomerService/GetCustomers", this.getCustomersOnSuccess, this.getCustomersOnError);
        }

        this.prepareSearch = function () {

            var inquiry = new Object();

            inquiry.currentPageNumber = vm.currentPageNumber;
            inquiry.sortExpression = vm.sortExpression;
            inquiry.sortDirection = vm.sortDirection;
            inquiry.pageSize = vm.pageSize;

            return inquiry;

        }

        this.getCustomersOnSuccess = function (response) {
            vm.customers = response.customers;
            vm.totalCustomers = response.totalRows;
            vm.totalPages = response.totalPages;
        }

        this.getCustomersOnError = function (response) {
            alertService.RenderErrorMessage(response.ReturnMessage);
        }

        this.getTemplate = function (customer) {
            if (customer.id === vm.selected.id) {
                return 'edit';
            }
            else return 'display';
        };

        this.editCustomer = function (customer) {
            vm.selected = angular.copy(customer);
        };

        this.reset = function () {
            vm.selected = {};
        };

        this.addCustomer = function () {
            vm.customerData = {
                payerAccountNumber: vm.payerAccountNumber,
                payerAccountName: vm.payerAccountName,
                email: vm.email
            };
            vm.customers.push(vm.customerData);
            console.log(vm.customers);
        };

        this.addCustomertoDB = function (customer) {
            ajaxService.ajaxPost(customer, "api/CustomerService/addCustomertoDB", function () {
                console.log("emp added");
                vm.getCustomers();
            }, function () {
                console.log("emp adding failed");
            });

        };

        this.updateCustomer = function (customer) {

            ajaxService.ajaxPost(customer, "api/CustomerService/updateCustomer",
                function (data) {
                    console.log(data);
                    vm.reset();
                    vm.getCustomers();
                },
                function () {
                    console.log("Error updating");
                }
            );
           
        };

        
        this.deleteCustomer = function (customer) {
        
            ajaxService.ajaxPost(customer, "api/CustomerService/deleteCustomer",
                function (data) {
                    console.log(data);
                    vm.getCustomers();
                },
                function () {
                    console.log("Error deleting");
                }
            );
        };

        this.addOrUpdateCustomer = function (customer) {
            if (customer.id) {
                vm.updateCustomer(customer);
            } else {
                vm.addCustomertoDB(customer);
            }

        }

        this.getCustomers = function () {
            debugger;
            ajaxService.ajaxPost("api/CustomerService/getCustomer",
                function (data) {
                    vm.customers = data;
                }, 
                function () {
                    console.log("Error adding to DB");
                }
            );
        };

    }]);
