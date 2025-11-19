<template>
    <div class="repair-history-container">
        <h2>Repair History Summary</h2>
        <div v-if="history.service" class="summary">
            <div class="summary-section">
                <p><strong>Service:</strong> {{ history.service }}</p>
                <p><strong>Service Cost (per hour):</strong> {{ history.price }} PLN</p>
                <p><strong>Total Service Cost:</strong> {{ totalServiceCost }} PLN</p>
                <p><strong>Hours Spent:</strong> {{ history.spentHours }} hours</p>
            </div>

            <div class="total-section">
                <p><strong>Total Parts Cost:</strong> {{ totalPartsCost }} PLN</p>
                <p><strong>Total Service Cost:</strong> {{ totalServiceCost }} PLN</p>
                <p><strong>Total amount:</strong> {{ totalRepairCost }} PLN</p>
            </div>
        </div>

        <div v-else>
            <p>Loading repair history...</p>
        </div>

        <h3>Parts and Materials Used</h3>
        <ul class="history-items">
            <li v-for="(item, index) in history.historyItems" :key="index" class="history-item">
                <p><strong>Item Name:</strong> {{ item.name }}</p>
                <p><strong>Price:</strong> {{ item.price }} PLN</p>
                <p><strong>Quantity:</strong> {{ item.quantity }}</p>
                <p><strong>Total Item Cost:</strong> {{ (item.price * item.quantity).toFixed(2) }} PLN</p>
            </li>
        </ul>
    </div>
</template>

<script>
import { getRepairHistory } from '@/ApiCommunication/RepairHistory';
export default {
    name: 'RepairHistoryDetails',
    props: {
        id: String
    },
    data() {
        return {
            history: {}
        };
    },
    mounted() {
        this.getHistory();
    },
    methods: {
        async getHistory() {
            try {
                this.history = await getRepairHistory(this.id);
            } catch (error) {
                console.error('Error fetching repair history:', error);
            }
        }
    },
    computed: {
        totalServiceCost() {
            return parseFloat((this.history.price * this.history.spentHours).toFixed(2));
        },
        totalPartsCost() {
            return parseFloat(this.history.historyItems.reduce((sum, item) => {
                return sum + (parseFloat(item.price || 0) * parseInt(item.quantity || 1));
            }, 0).toFixed(2));
        },
        totalRepairCost() {
            return parseFloat((this.totalServiceCost + this.totalPartsCost).toFixed(2));
        }
    },
};
</script>

<style scoped>
.repair-history-container {
    background-color: #f9f9f9;
    padding: 2em;
    border-radius: 8px;
    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
}

h2 {
    font-size: 1.6em;
    color: #333;
    margin-bottom: 1em;
}

.summary, .total-section, .summary-section {
    margin-bottom: 1em;
}

.summary-section, .total-section {
    padding: 1em;
    border: 1px solid #ddd;
    border-radius: 5px;
    background-color: #fff;
}

.total-section {
    margin-top: 1em;
    font-weight: bold;
    background-color: #f3f3f3;
}

h3 {
    font-size: 1.4em;
    color: #555;
    margin-top: 1.5em;
    margin-bottom: 0.5em;
}

.history-items {
    list-style-type: none;
    padding: 0;
}

.history-item {
    border: 1px solid #ddd;
    padding: 1em;
    margin-bottom: 1em;
    border-radius: 8px;
    background-color: #ffffff;
    box-shadow: 0 1px 5px rgba(0, 0, 0, 0.05);
}

.history-item p {
    margin: 0.3em 0;
}

strong {
    color: #222;
}
</style>
