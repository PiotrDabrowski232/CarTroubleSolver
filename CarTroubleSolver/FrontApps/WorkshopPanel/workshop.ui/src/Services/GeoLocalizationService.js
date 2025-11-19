export function getLocation() {
  return new Promise((resolve, reject) => {
    if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition(resolve, reject);
    } else {
      reject(new Error('Geolocalization'));
    }
  });
}

export async function reverseGeocode(latitude, longitude) {
  try {
    const response = await fetch(`https://nominatim.openstreetmap.org/reverse?format=json&lat=${latitude}&lon=${longitude}&zoom=18&addressdetails=1`);
    const data = await response.json();
    return {
      streetName: data.address.road || '',
      streetNumber: data.address.house_number || '',
      city: data.address.city || data.address.town || data.address.village || '',
      postalCode: data.address.postcode || '',
      country: data.address.country || '',
      province: data.address.state || '',
    };
  } catch (error) {
    throw new Error('Error while localize device');
  }
}

