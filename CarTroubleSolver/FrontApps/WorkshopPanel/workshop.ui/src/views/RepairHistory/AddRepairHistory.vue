<template>
    <div class="repair-history-container">
        <h2>Add repair history</h2>

        <section class="section">
            <h3>Service performed</h3>
            <div class="form-group">
                <label for="service-type">Service type</label>
                <span>{{ service.serviceType }}</span>
            </div>
            <div class="form-group">
                <label for="service-price">Service price:</label>
                <span>{{ service.price }} PLN</span>
            </div>
            <div class="form-group">
                <label for="hours-spent">Number of hours:</label>
                <input type="number" v-model="hoursSpent" min="0" />
            </div>
        </section>

        <hr class="section-divider" />

        <section class="section">
            <h3>Purchased items</h3>
            <div v-for="(item, index) in purchasedItems" :key="index" class="item-container">
                <div class="form-group">
                    <label for="item-name">Item name:</label>
                    <input type="text" v-model="item.name" placeholder="Nazwa elementu" />
                    <span v-if="!isNameValid(item.name)" class="error">{{ nameError }}</span>
                </div>
                <div class="item-price-quantity">
                    <div class="form-group">
                        <label for="item-price">Item price:</label>
                        <input type="number" v-model="item.price" placeholder="Cena elementu" min="0" />
                        <span v-if="!isPriceValid(item.price)" class="error">{{ priceError }}</span>
                    </div>
                    <div class="form-group">
                        <label for="item-quantity">Quantity:</label>
                        <input type="number" v-model="item.quantity" placeholder="Ilość" min="1" />
                        <span v-if="!isQuantityValid(item.quantity)" class="error">{{ quantityError }}</span>
                    </div>
                </div>
                <button class="remove-item-btn" @click="removeItem(index)">Delete</button>
            </div>
            <button class="add-item-btn" @click="addItem">Add element</button>
        </section>

        <hr class="section-divider" />

        <section class="section">
            <h3>Summary of costs</h3>
            <div class="cost-summary">
                <p><strong>Total cost of service:</strong> {{ totalServiceCost }} PLN</p>
                <p><strong>Total cost of purchased items:</strong> {{ totalPartsCost }} PLN</p>
                <p><strong>Total cost of repair:</strong> {{ totalRepairCost }} PLN</p>
            </div>
        </section>

        <button class="close-repair-btn" @click="closeRepair" :disabled="!isFormValid">Close the repair</button>
    </div>
</template>

<script>
import { getServicePriceDetails, changeAccidentStatus } from '@/ApiCommunication/Accident';

export default {
    name: 'AddRepairHistory',
    props: {
        id: String,
    },
    data() {
        return {
            service: {},
            hoursSpent: 1,
            purchasedItems: [],
            nameError: "Name must contain letters",
            priceError: "Price should be greater than 0",
            quantityError: "Quantity should be greater than 0"
        };
    },
    mounted() {
        this.getServicePrice();
    },
    methods: {
        async getServicePrice() {
            this.service = await getServicePriceDetails(this.id);
        },
        addItem() {
            this.purchasedItems.push({ name: '', price: 0, quantity: 1 });
        },
        removeItem(index) {
            this.purchasedItems.splice(index, 1);
        },
        async closeRepair() {
            const repairs = {
                Service: this.service.serviceType,
                Price: this.service.price,
                SpentHours: this.hoursSpent,
                HistoryItems: []
            };
            if (this.purchasedItems.length > 0)
                this.purchasedItems.forEach(item => {
                    repairs.HistoryItems.push({
                        Name: item.name,
                        Price: item.price,
                        Quantity: item.quantity
                    });
                });

            console.log(repairs)

            var result = await changeAccidentStatus(this.id, repairs)

            if (result) {
                console.log("bravo")
            }
        },
        isNameValid(name) {
            return !!name.trim();
        },
        isPriceValid(price) {
            return price > 0;
        },
        isQuantityValid(quantity) {
            return quantity > 0;
        }
    },
    computed: {
        totalServiceCost() {
            return parseFloat((this.hoursSpent * this.service.price).toFixed(2));
        },
        totalPartsCost() {
            return parseFloat(this.purchasedItems.reduce((sum, item) => {
                return sum + (parseFloat(item.price || 0) * parseInt(item.quantity || 1));
            }, 0).toFixed(2));  
        },
        totalRepairCost() {
            return parseFloat((this.totalServiceCost + this.totalPartsCost).toFixed(2));
        },
        isFormValid() {
            return this.purchasedItems.every(item =>
                this.isNameValid(item.name) &&
                this.isPriceValid(item.price) &&
                this.isQuantityValid(item.quantity)
            );
        }
    },
};
</script>

<style scoped>
.repair-history-container {
    max-width: 80vw;
    margin: 0 auto;
    font-family: Arial, sans-serif;
    padding: 2rem;
}

h2 {
    text-align: center;
    font-size: 2.4rem;
    margin-bottom: 2rem;
}

.section h3 {
    font-size: 2rem;
    margin-bottom: 1rem;
    color: #333;
}

.form-group {
    margin-bottom: 1.5rem;
}

label {
    font-weight: bold;
    display: block;
    margin-bottom: 0.5rem;
}

input[type="number"],
input[type="text"] {
    width: 100%;
    padding: 1rem;
    font-size: 1.6rem;
    border: 1px solid #ddd;
    border-radius: 0.4rem;
    margin-bottom: 1rem;
}

input[type="number"]:focus,
input[type="text"]:focus {
    outline: none;
    border-color: #007BFF;
}

button {
    padding: 1rem 2rem;
    font-size: 1.6rem;
    cursor: pointer;
    border-radius: 0.4rem;
}

.add-item-btn {
    background-color: #28a745;
    color: white;
    border: none;
    margin-top: 1rem;
}

.add-item-btn:hover {
    background-color: #218838;
}

.remove-item-btn {
    background-color: #dc3545;
    color: white;
    border: none;
    margin-top: 1rem;
}

.remove-item-btn:hover {
    background-color: #c82333;
}

.close-repair-btn:disabled {
    background-color: darkslategray;
    pointer-events: none;

}

.close-repair-btn:disabled:hover {
    background-color: darkslategray;
    pointer-events: none;
}

.close-repair-btn {
    background-color: #007bff;
    color: white;
    border: none;
    padding: 1.2rem 3rem;
    font-size: 1.8rem;
    cursor: pointer;
    display: block;
    width: 100%;
    margin-top: 2rem;
    border-radius: 0.5rem;
    margin-bottom: 5rem;
}

.close-repair-btn:hover {
    background-color: #0056b3;
}

hr.section-divider {
    border: 0;
    border-top: 2px solid #ddd;
    margin: 2rem 0;
}

.cost-summary {
    font-size: 1.8rem;
}

.cost-summary p {
    margin: 0.5rem 0;
    color: #333;
}

.cost-summary strong {
    font-weight: bold;
}

.item-price-quantity {
    display: flex;
    gap: 1rem;
}

.item-price-quantity .form-group {
    flex: 1;
}

.error {
    color: red;
    font-size: 1.2rem;
}
</style>
