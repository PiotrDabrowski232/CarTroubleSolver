<template>
    <div class="repair-history-container">
        <h2>Repair History</h2>
        <div v-if="repairs && repairs.service" class="repair-card">
            <div class="service-info">
                <h3><i class="fas fa-tools"></i> Service: {{ repairs.service }}</h3>
                <p><strong>Service Cots:</strong> {{ repairs.price }} PLN</p>
                <p><strong>Working time:</strong> {{ repairs.spentHours }} h</p>
            </div>

            <div class="parts-info">
                <h4><i class="fas fa-cogs"></i> Parts used in the repair:</h4>
                <ul class="parts-list">
                    <li v-for="(item, index) in repairs.historyItems" :key="index" class="part-item">
                        <span><strong>{{ item.name }}</strong></span>
                        <span>{{ item.price }} PLN x {{ item.quantity }} part</span>
                    </li>
                </ul>
            </div>

            <div class="cost-summary">
                <h4>Summary of costs</h4>
                <p><strong>Labor cost:</strong> {{ totalServiceCost }} PLN</p>
                <p><strong>Cost of parts:</strong> {{ totalPartsCost }} PLN</p>
                <p class="total-cost"><strong>Total cost of repair:</strong> {{ totalRepairCost }} PLN</p>
            </div>
        </div>

        <div v-else class="no-data">
            <p>No data to display.</p>
        </div>
    </div>
</template>

<script>
import { getRepairHistory } from '@/services/StatusCommunication';

export default {
    name: 'RepairHistory',
    props: {
        id: String
    },
    data() {
        return {
            repairs: {}
        }
    },
    mounted() {
        this.GetRepairs();
    },
    methods: {
        async GetRepairs() {
            this.repairs = await getRepairHistory(this.id);
        }
    },
    computed: {
        totalServiceCost() {
            return parseFloat((this.repairs.price * this.repairs.spentHours).toFixed(2));
        },
        totalPartsCost() {
            return parseFloat(this.repairs.historyItems.reduce((sum, item) => {
                return sum + (parseFloat(item.price || 0) * parseInt(item.quantity || 1));
            }, 0).toFixed(2));
        },
        totalRepairCost() {
            return parseFloat((this.totalServiceCost + this.totalPartsCost).toFixed(2));
        }
    }
}
</script>

<style scoped>
.repair-history-container {
    max-width: 600px;
    margin: 0 auto;
    padding: 20px;
    background-color: #f9f9f9;
    border-radius: 8px;
    box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
    font-family: Arial, sans-serif;
}

h2 {
    text-align: center;
    color: #2c3e50;
    margin-bottom: 20px;
}

.repair-card {
    background-color: #ffffff;
    border-radius: 8px;
    padding: 20px;
    margin-bottom: 20px;
    box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
}

.service-info h3, .parts-info h4, .cost-summary h4 {
    color: #2c3e50;
    display: flex;
    align-items: center;
    font-size: 18px;
    margin-bottom: 12px;
}

.service-info h3 i, .parts-info h4 i {
    color: #3498db;
    margin-right: 8px;
}

p, li {
    font-size: 16px;
    color: #34495e;
    margin: 6px 0;
}

.parts-info .parts-list {
    padding-left: 0;
    list-style-type: none;
}

.parts-info .part-item {
    display: flex;
    justify-content: space-between;
    padding: 8px 0;
    border-bottom: 1px solid #eaeaea;
}

.cost-summary p {
    margin: 8px 0;
}

.total-cost {
    font-size: 18px;
    color: #e74c3c;
    font-weight: bold;
}

.no-data {
    text-align: center;
    color: #95a5a6;
    font-size: 16px;
    padding: 20px;
}
</style>
