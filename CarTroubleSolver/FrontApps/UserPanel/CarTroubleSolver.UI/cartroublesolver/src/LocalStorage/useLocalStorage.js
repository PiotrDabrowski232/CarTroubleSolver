export function saveToLocalStorage(key, value) {
    window.localStorage.setItem(key, JSON.stringify(value));
  }
  
export function getFromLocalStorage(key) {
    const storageVal = window.localStorage.getItem(key);
    if (storageVal) {
      return JSON.parse(storageVal);
    }
    return null;
  }
  
