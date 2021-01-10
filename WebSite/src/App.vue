<template>
    <div id="app">
        <h3>{{actionName}}</h3>
        <UserEditControl :id="selectedUser.Id"
                         :username="selectedUser.Username"
                         :email="selectedUser.Email"
                         :password="selectedUser.Password"
                         :addUser="addUser"
                         :updateUser="updateUser"
                         :actionName ="actionName"
                         ></UserEditControl>
        <Grid :data="gridData"
              :columns="gridColumns"
              :deleteRow="deleteUser"
              :updateRow="getUserData"
              :addAction = "openAddUserForm"></Grid>
    </div>
</template>
<script>
    import Vue from 'vue';
    import axios from 'axios'
    import Grid from './components/Grid.vue'
    import UserEditControl from './components/UserEditControl.vue'
    import { SiteUrl } from './components/Settings/SiteUrl.js';
    Vue.component('Grid', Grid);
    Vue.component('UserEditControl', UserEditControl);
    export default {
        name: 'app',
        data() {
            return {
                gridData: [],
                gridColumns: ['name', 'email'],
                selectedUser: {},
                actionName: "Add",
                oldUserData: {}
            }
        },
        methods: {
            failed(error) {
                alert(error.response.data.error);
                this.selectedUser = {
                    Id: this.selectedUser.Id,
                    Username: this.oldUserData.Name,
                    Email: this.oldUserData.Email,
                    Password: this.oldUserData.Password
                };
            },
            deleteUser(id) {
                axios
                    .delete(SiteUrl.Users_Delete(id))
                    .then(this.userIsDeleted)
                    .catch(this.failed);
            },
            getUserData(id) {
                axios
                    .get(SiteUrl.Users_Get(id))
                    .then(this.setUserEditData)
                    .catch(this.failed);
            },
            openAddUserForm() {
                this.selectedUser = {};
                this.actionName = "Add";
            },
            addUser(userData) {
                this.oldUserData = userData;

                axios.post(SiteUrl.Users_Add(), userData)
                    .then(response => {
                        alert("User was succesfully added");
                        this.refreshUsersTable();
                    })
                    .catch(this.failed);
            },
            updateUser(userData) {
                this.oldUserData = userData;
                
                axios.put(SiteUrl.Users_Update(this.selectedUser.Id), userData)
                    .then(response => {
                        alert("User was succesfully updated");
                        this.refreshUsersTable();
                    })
                    .catch(this.failed);
            },
            setUserEditData(response) {
                this.selectedUser = {
                    Id: response.data.id,
                    Username: response.data.name,
                    Email: response.data.email
                };
                this.actionName = "Edit";
            },
            userIsDeleted() {
                alert("User was succesfully deleted");
                this.refreshUsersTable();
            },
            refreshUsersTable() {
                axios
                    .get(SiteUrl.Users_GetList())
                    .then(response => this.gridData = response.data);
            }
        },
        components: {
            Grid,
            UserEditControl
        },
        mounted() {
            this.refreshUsersTable();
        }
    };
</script>
<style>
</style>
