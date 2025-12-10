// ------------------------------
// WEATHER FETCHER (Auto Location)
// ------------------------------

const apiKey = "55450291958663f5c1d03d722ae8c0e6";

// When the page loads:
window.onload = function () {
    getUserLocation();
};

function getUserLocation() {
    if (!navigator.geolocation) {
        document.getElementById("weatherStatus").innerText =
            "Geolocation not supported.";
        return;
    }

    navigator.geolocation.getCurrentPosition(success, error);
}

function success(position) {
    const lat = position.coords.latitude;
    const lon = position.coords.longitude;

    getWeather(lat, lon);
}

function error(err) {
    document.getElementById("weatherStatus").innerText =
        "Unable to access location.";
}

function getWeather(lat, lon) {
    const url =
        `https://api.openweathermap.org/data/2.5/weather?lat=${lat}&lon=${lon}&appid=${apiKey}&units=metric`;

    fetch(url)
        .then(response => response.json())
        .then(data => {
            document.getElementById("weatherStatus").innerText =
                data.weather[0].description.toUpperCase();

            document.getElementById("weatherTemp").innerText =
                `${data.main.temp}°C`;

            document.getElementById("weatherCity").innerText =
                `📍 ${data.name}`;
        })
        .catch(err => {
            document.getElementById("weatherStatus").innerText =
                "Error loading weather.";
        });
}
