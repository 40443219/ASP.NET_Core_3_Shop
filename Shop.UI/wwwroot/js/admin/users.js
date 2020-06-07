let app = new Vue({
    el: "#app",
    data() {
        return {
            username: "",
            password: ""
        }
    },
    mounted() {
    },
    methods: {
        createUser() {
            this.loading = true;
            axios.post('/users', {
                    username: this.username,
                    password: this.password
                })
                .then((res) => {
                    console.log(res);
                    this.products = res.data;
                })
                .catch(err => {
                    console.error(err);
                })
                .then(() => {
                    this.loading = false;
                });
        }
    }
});