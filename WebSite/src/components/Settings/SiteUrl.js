export class SiteUrl {
    static GetBaseUrl() {
        return "https://localhost:44371";
    }

    static Users_GetList() {
        return this.GetBaseUrl() + "/api/users";
    }

    static Users_Get(id) {
        return this.GetBaseUrl() + "/api/users/" + id;
    }

    static Users_Add() {
        return this.GetBaseUrl() + "/api/users/";
    }

    static Users_Update(id) {
        return this.GetBaseUrl() + "/api/users/" + id;
    }

    static Users_Delete(id) {
        return this.GetBaseUrl() + "/api/users/" + id;
    }
}