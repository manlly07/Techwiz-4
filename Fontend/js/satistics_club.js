const currentURL = window.location.href;
const url = new URL(currentURL);
const searchParams = new URLSearchParams(url.search)
const clubId = url.search.split('?id=')[1]
console.log(clubId);
let api = `https://localhost:7023/api/Match/matches/${clubId}`

const handleMatchDetail = (id) => {
    console.log(id);
    window.location.href = './match_detail.html?id='+id
}
const fetchMatchByClub = async () => {
    const animate = document.getElementById("animate")
    animate.classList.remove('visually-hidden')
    const response = await fetch(api, 
        {
            method: 'GET',
        }).then(response => response.json())
    console.log(response);
    render(response.matches)
    animate.classList.add('visually-hidden')
}

fetchMatchByClub()

const render = (data) => {
    // const heading = document.getElementById('club-heading')
    // heading.innerText = heading
    const list = document.getElementById('satistics_club_match')
    let html = data.map(item => {
        return (
            `
                <div class="col-6 border py-4" onclick="handleMatchDetail(${item.id})">
                    <div class="row">
                    <div class="col-8 border-end">
                        <div class="row justify-content-center align-items-center my-2">
                        <div class="col-2 mx-auto">
                            <img src="${item.homeTeam.crest}" alt="" width="36" height="36">
                        </div>
                        <div class="col-9 fs-6">
                            <a href="./satistics_club.html?id=${item.homeTeam.id}">
                                ${item.homeTeam.name}
                            </a>
                        </div>
                        <div class="col-1 fs-6">${item.score.fullTime.home ?? ''}</div>
                        </div>
                        <div class="row justify-content-center align-items-center my-2">
                        <div class="col-2 mx-auto">
                            <img src="${item.awayTeam.crest}" alt="" width="36" height="36">
                        </div>
                        <div class="col-9 fs-6 fw-lighter text-muted">
                            <a href="./satistics_club.html?id=${item.awayTeam.id}">
                                ${item.awayTeam.name}
                            </a>
                        </div>
                        <div class="col-1 fs-6 text-muted">${item.score.fullTime.away ?? ''}</div>
                        </div>
                    </div>
                        <div class="col-4 d-flex flex-column align-items-center justify-content-center text-center">
                            <span class="py-0">${item.score.winner ? 'FT': getDate(item.utcDate)}</span>
                            <span>${item.score.winner ? getDate(item.utcDate) : getHours(item.utcDate)}</span>
                        </div>
                    </div>
                </div>
            `
        )
    })
    list.innerHTML = html.join('')
}

const getDate = (day) => {
    const dateString = day;
    const date = new Date(dateString);
    
    const options = { year: 'numeric', month: 'short', day: 'numeric' };
    const formattedDate = date.toLocaleDateString('en-US', options);
    return formattedDate
}


const getHours = (date) => {

    const dateTimeString = date;
    const dateTime = new Date(dateTimeString);
    const options = { hour: '2-digit', minute: '2-digit' };
    const formattedTime = dateTime.toLocaleTimeString('en-US', options);
    return formattedTime  
} 