var map = L.map("locationMap");
async function getlon_len(zipcode) {

    map.setView([0, 0], 1);

    console.log(map);

    const attribution = '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors';
    const tileUrl = 'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png';
    const tiles = L.tileLayer(tileUrl, { attribution });
    tiles.addTo(map);

    const response = await fetch('https://nominatim.openstreetmap.org/search?format=json&limit=1&q=india,' + zipcode);
        const data = await response.json();
        const { lat, lon } = data[0];
        map.flyTo([lat, lon], 15);
        L.marker([lat, lon]).addTo(map);

    console.log(lat);

}