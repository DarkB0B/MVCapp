function addFavourite(teamId) {
    fetch(`/Home/AddFavourite?teamId=${teamId}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Failed to add team to favourites');
            }
            console.log('Team added to favoruites');
        })
        .catch(error => {
            console.error('Error adding team to favourites:', error);
        });
}
function removeFavourite(teamId) {
    fetch(`/Home/RemoveFavourite?teamId=${teamId}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Failed to remove team from favourites');
            }
            console.log('Team removed from favourites');
        })
        .catch(error => {
            console.error('Error removing team from favourites:', error);
        });
}

function toggleFavouriteButton(button) {
    if (button.classList.contains('add-favourite-button')) {
        button.classList.remove('add-favourite-button');
        button.classList.add('remove-favourite-button');
        button.innerHTML = '<img class ="star" src="/icons/star-fill.svg" alt="Remove from Favourites">'; 
        addFavourite(button.dataset.teamid);
    } else {
        button.classList.remove('remove-favourite-button');
        button.classList.add('add-favourite-button');
        button.innerHTML = '<img class ="star" src="/icons/star.svg" alt="Add to Favourites">';
        removeFavourite(button.dataset.teamid);
    }
}