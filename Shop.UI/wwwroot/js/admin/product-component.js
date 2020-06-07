Vue.component('product-manager', {
    template: `
    <div>
        <div v-if="!editing">
            <button class="button" @click="newProduct">New product</button>
            <table class="table">
                <tr>
                    <th>Id</th>
                    <th>Product name</th>
                    <th>Price</th>
                    <th>Actions</th>
                </tr>
                <tr v-for="(product, index) in products">
                    <td>
                        {{product.id}}
                    </td>
                    <td>
                        {{product.name}}
                    </td>
                    <td>
                        {{product.value}}
                    </td>
                    <td>
                        <!-- <a @click="editProduct(product, index)">Edit</a> -->
                        <a href="#" @click="editProduct(product.id, index)">Edit</a>
                        <a href="#" @click="deleteProduct(product.id, index)">Remove</a>
                    </td>
                </tr>
            </table>
        </div>

        <div v-else>
            <div class="field">
                <label class="label">Product name</label>
                <div class="control">
                    <input class="input" v-model="productModel.name" />
                </div>
            </div>
            <div class="field">
                <label class="label">Product description</label>
                <div class="control">
                    <input class="input" v-model="productModel.description" />
                </div>
            </div>
            <div class="field">
                <label class="label">Product value</label>
                <div class="control">
                    <input class="input" v-model="productModel.value" />
                </div>
            </div> 
            <div class="field">
                <div class="control">
                    <button class="button is-success" @click="createProduct" v-if="!productModel.id">Create Product</button>
                    <button class="button is-warning" @click="updateProduct" v-else>Update Product</button>
                    <button class="button is-danger" @click="cancel">Cancel</button>
                </div>
            </div>
        </div>
    </div>`,
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
        };
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