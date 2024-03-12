// api.js

import axios from 'axios';

// Crie uma instância do Axios com a baseUrl configurada
const api = axios.create({
  baseURL: 'http://localhost:3000' // Substitua a porta 3000 pela porta do seu servidor JSON
});

// Exporte a instância do Axios para uso em outros arquivos
export default api;