
import Cookies from 'js-cookie';

const TOKEN_KEY = "Authority";

export default {
  getToken() {
    return Cookies.get(TOKEN_KEY);
  },

  setToken(token) {
    Cookies.set(TOKEN_KEY, token, { expires: 1, sameSite: 'strict' }); 
  },

  removeToken() {
    Cookies.remove(TOKEN_KEY);
  },
};
