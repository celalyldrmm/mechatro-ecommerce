// startCountdown İndirim

function startCountdown() {
	const daysElement = document.getElementById('days');
	const hoursElement = document.getElementById('hours');
	const minutesElement = document.getElementById('minutes');
	const secondsElement = document.getElementById('seconds');

	let days = 2;
	let hours = 10;
	let minutes = 34;
	let seconds = 60;

	const countdown = setInterval(() => {
		seconds--;
		if (seconds < 0) {
			seconds = 59;
			minutes--;
			if (minutes < 0) {
				minutes = 59;
				hours--;
				if (hours < 0) {
					hours = 23;
					days--;
					if (days < 0) {
						clearInterval(countdown);
					}
				}
			}
		}

		daysElement.textContent = padNumber(days);
		hoursElement.textContent = padNumber(hours);
		minutesElement.textContent = padNumber(minutes);
		secondsElement.textContent = padNumber(seconds);
	}, 1000);
}

function padNumber(number) {
	return number.toString().padStart(2, '0');
}

// Sayfa yüklendiğinde countdown'u başlat
document.addEventListener('DOMContentLoaded', startCountdown);

