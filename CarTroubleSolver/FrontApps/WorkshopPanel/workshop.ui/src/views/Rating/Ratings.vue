<template>
    <div class="rating-container">
      <div class="workshop-info">
        <h2>{{ Rating.workshopName }}</h2>
        <p class="average-rating">Średnia ocena: <span>{{ Rating.average }} / 5</span></p>
      </div>
  
      <div v-for="(item, index) in Rating.ratingItems" :key="index" class="rating-item">
        <div class="rating-header">
          <div class="user-info">
            <strong>{{ item.userName }}</strong>
            <span class="rating-date">{{ new Date(item.date).toLocaleDateString() }}</span>
          </div>
          <span class="rating-score">{{ item.rate }} / 5</span>
        </div>
        <p class="rating-content">{{ item.content }}</p>
      </div>
    </div>
  </template>
  
  <script>
  import { GetRatings } from '@/ApiCommunication/Rating';
  
  export default {
    name: 'Rating',
    data() {
      return {
        Rating: {}
      };
    },
    methods: {
      async GetRatings() {
        this.Rating = await GetRatings();
      }
    },
    mounted() {
      this.GetRatings();
    }
  };
  </script>
  
  <style scoped>
  .rating-container {
    max-width: 600px;
    margin: 20px auto;
    background: linear-gradient(135deg, #f3f4f6, #ffffff);
    border-radius: 12px;
    padding: 20px;
    box-shadow: 0px 4px 12px rgba(0, 0, 0, 0.1);
    font-family: Arial, sans-serif;
  }
  
  .workshop-info {
    text-align: center;
    border-bottom: 2px solid #e2e8f0;
    padding-bottom: 10px;
    margin-bottom: 15px;
  }
  
  .workshop-info h2 {
    font-size: 1.8em;
    margin-bottom: 5px;
    color: #1f2937;
  }
  
  .average-rating {
    font-size: 1.1em;
    color: #4b5563;
  }
  
  .average-rating span {
    font-weight: bold;
    color: #3b82f6;
  }
  
  .rating-item {
    background: #f9fafb;
    border-radius: 8px;
    padding: 15px;
    margin-bottom: 15px;
    box-shadow: 0px 2px 6px rgba(0, 0, 0, 0.05);
  }
  
  .rating-header {
    display: flex;
    justify-content: space-between;
    align-items: center;
    margin-bottom: 8px;
  }
  
  .user-info {
    display: flex;
    flex-direction: column;
  }
  
  .user-info strong {
    font-size: 1.1em;
    color: #111827;
  }
  
  .rating-date {
    font-size: 0.9em;
    color: #6b7280;
  }
  
  .rating-score {
    font-size: 1em;
    font-weight: bold;
    color: #f59e0b;
  }
  
  .rating-content {
    font-size: 1em;
    color: #374151;
    margin-top: 5px;
    padding-left: 10px;
    border-left: 3px solid #3b82f6;
  }
  </style>
  