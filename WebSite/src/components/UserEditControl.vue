<template>
    <div>

        <div role="dialog" aria-labelledby="dialog-title">
            <div class="card">
                <label for="modal-control" class="modal-close"></label>
                <p class="section">
                    <input v-model="username" type="text" placeholder="Username">
                    <input v-model="email" type="email" placeholder="Email">
                    <input v-model="password" type="password" placeholder="Password">
                    <button @click="saveUser">{{actionName}}</button>
                    <button @click="closeModal">Cancel</button>
                </p>
            </div>
        </div>
    </div>
</template>

<script>
    export default {
        name: "UserEditControl",
        props: {
            id: {},
            username: "",
            email: "",
            password: "",
            addUser: { type: Function },
            updateUser: { type: Function },
            actionName: ""
        },
        data() {
            return {
                visible: false
            }
        },
        methods: {
            saveUser() {
                var userData = {};
                userData.Name = this.username;
                userData.Email = this.email;
                userData.Password = this.password;

                if (this.actionName == "Edit") {
                    userData.Id = this.Id;
                    this.updateUser(userData);
                } else {
                    userData.Id = 0;
                    this.addUser(userData);
                }
                this.username = '';
                this.email = '';
                this.id = 0;
                this.password = '';
                this.closeModal();
            },
            closeModal() {
                this.visible = false;
            }
        }
    }
</script>

<style>
    .card {
        input{
            width: 98%;
        }
        button {
            width: 44%;
            margin-left: 2%;
        }
    }
</style>
