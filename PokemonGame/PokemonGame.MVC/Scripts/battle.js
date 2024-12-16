function playSound(effect) {
    const audio = new Audio(`/Content/Sounds/${effect}.mp3`);
    audio.play();
}

function animateAttack(attackerClass, defenderClass, isCritical) {
    const attacker = document.querySelector(attackerClass);
    const defender = document.querySelector(defenderClass);

    playSound(isCritical ? 'critical-hit' : 'attack');

    attacker.classList.add('attack');
    setTimeout(() => {
        attacker.classList.remove('attack');
        defender.classList.add('damage');
        setTimeout(() => defender.classList.remove('damage'), 500);
    }, 500);
}

