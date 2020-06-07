let app = new Vue({
    el: "#app",
    data() {
        return {
            products: [],
            selectedProduct: null,
            newStock: {
                productId: 0,
                description: "Size",
                quantity: 10
            },
            loading: false
        }
    },
    mounted() {
        this.getStocks();
    },
    methods: {
        getStocks() {
            this.loading = true;
            axios.get('/stocks')
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
        },
        selectProduct(product) {
            this.selectedProduct = product;
            this.newStock.productId = product.id;
        },
        updateStocks() {
            this.loading = true;
            axios.put(`/stocks`, {
                    stocks: this.selectedProduct.stocks.map(x => {
                        return {
                            id: x.id,
                            description: x.description,
                            quantity: x.quantity,
                            productId: this.selectedProduct.id
                        }
                    })
                })
                .then((res) => {
                    console.log(res);
                })
                .catch(err => {
                    console.error(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },
        deleteStock(id, index) {
            this.loading = true;
            axios.delete(`/stocks/${id}`)
                .then((res) => {
                    console.log(res);
                    this.selectedProduct.stocks.splice(index, 1);
                })
                .catch(err => {
                    console.error(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },
        addStock() {
            this.loading = true;
            axios.post('/stocks', this.newStock)
                .then((res) => {
                    console.log(res);
                    this.selectedProduct.stocks.push(res.data);
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