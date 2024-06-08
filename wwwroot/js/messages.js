document.addEventListener('DOMContentLoaded', function () {
    const inboxBtn = document.getElementById('inboxBtn');
    const sentBtn = document.getElementById('sentBtn');
    const inboxSection = document.getElementById('inboxSection');
    const sentSection = document.getElementById('sentSection');

    inboxBtn.addEventListener('click', function () {
        inboxSection.style.display = 'block';
        sentSection.style.display = 'none';
        inboxBtn.classList.add('btn-inbox');
        inboxBtn.classList.remove('btn-sent');
        sentBtn.classList.add('btn-sent');
        sentBtn.classList.remove('btn-inbox');
    });

    sentBtn.addEventListener('click', function () {
        inboxSection.style.display = 'none';
        sentSection.style.display = 'block';
        sentBtn.classList.add('btn-inbox');
        sentBtn.classList.remove('btn-sent');
        inboxBtn.classList.add('btn-sent');
        inboxBtn.classList.remove('btn-inbox');
    });
});