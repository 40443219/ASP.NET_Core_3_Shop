let app = new Vue({
    el: '#app',
    data() {
        return {
            // price: 0,
            // showPrice: true,
            editing: false,
            loading: false,
            productModel: {
                name: 'Product name',
                description: 'Product description',
                value: 100.99
            },
            products: [],
            objectIndex: 0
        }
    },
    mounted(){
        this.getProducts();
    },
    methods: {
        // togglePrice: function() {
        //     this.showPrice = !this.showPrice;
        // },
        getProduct(id) {
            this.loading = true;
            axios.get(`/products/${id}`)
                .then(res => {
                    console.log(res);
                    const product = res.data;
                    this.productModel = {
                        id: product.id,
                        name: product.name,
                        description: product.description,
                        value: product.value
                    }
                })
                .catch(err => {
                    console.error(err);
                }).then(() => {
                    this.loading = false;
                });
        },
        getProducts() {
            this.loading = true;
            axios.get('/products')
                .then(res => {
                    console.log(res);
                    this.products = res.data;
                })
                .catch(err => {
                    console.error(err);
                }).then(() => {
                    this.loading = false;
                });
        },
        newProduct() {
            this.editing = true;
            this.productModel.id = 0;
        },
        createProduct() {
            this.loading = true;
            axios.post('/products', this.productModel)
                .then(res => {
                    console.log(res);
                    this.products.push(res.data);
                })
                .catch(err => {
                    console.error(err);
                })
                .then(() => {
                    this.loading = false;
                    this.editing = false;
                });
        },
        updateProduct() {
            this.loading = true;
            axios.put('/products', this.productModel)
                .then(res => {
                    console.log(res);
                    this.products.splice(this.objectIndex, 1, res.data);
                })
                .catch(err => {
                    console.error(err);
                })
                .then(() => {
                    this.loading = false;
                    this.editing = false;
                });
        },
        deleteProduct(id, index) {
            this.loading = true;
            axios.delete(`/products/${id}`)
                .then(res => {
                    console.log(res);
                    this.products.splice(index, 1);
                })
                .catch(err => {
                    console.error(err);
                }).then(() => {
                    this.loading = false;
                });
        },
        // editProduct(product, index) {
        //     this.objectIndex = index;
        //     this.productModel = {
        //         id: product.id,
        //         name: product.name,
        //         description: product.description,
        //         value: product.value
        //     }
        // }
        editProduct(id, index) {
            this.objectIndex = index;
            this.getProduct(id);
            this.editing = true;
        },
        cancel() {
            this.editing = false;
        }
    },
    computed: {
        // formatPrice: function() {
        //     return `$${this.price}`;
        // }
    }
});