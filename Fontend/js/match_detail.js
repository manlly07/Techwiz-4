const currentURL = window.location.href;
const url = new URL(currentURL);
const searchParams = new URLSearchParams(url.search)
let matchId
let api

if (searchParams) {
    matchId = url.search.split('?id=')[1]
    console.log(matchId);
    api = `https://localhost:7023/api/Match/detail/${matchId}`
}

const fetchDetailClub = async () => {
    const response = await fetch(api,
        {
            method: 'GET',
        }).then(response => response.json());
    console.log(response);
    render(response)
}
if (searchParams) {
    fetchDetailClub()
}

const render = (data) => {
    let competition = document.getElementById('competition-heading')
    competition.innerText = data.competition.name + " - " + getDate(data.utcDate)
    let imageHome = document.getElementById('image-home')
    imageHome.src = data.homeTeam.crest
    let imageAway = document.getElementById('image-away')
    imageAway.src = data.awayTeam.crest
    let nameHome = document.getElementById('name-home')
    nameHome.innerText = data.homeTeam.name
    let nameAway = document.getElementById('name-away')
    nameAway.innerText = data.awayTeam.name
    let scoreHome = document.getElementById('score-home')
    scoreHome.innerText = data.score.fullTime.home
    let scoreAway = document.getElementById('score-away')
    scoreAway.innerText = data.score.fullTime.away

    let total = data.odds.homeWin + data.odds.awayWin + data.odds.draw
    let roleHome = document.getElementById('role-home')
    roleHome.style.width = data.odds.homeWin/total * 100 + '%' 
    let roleAway = document.getElementById('role-away')
    roleAway.style.width = data.odds.awayWin/total * 100 + '%'
    let roleDraw = document.getElementById('role-draw')
    roleDraw.style.width = data.odds.draw/total * 100 + '%'

    let homeTitle = document.getElementById('home-title')
    homeTitle.innerText = data.homeTeam.name

    let awayTitle = document.getElementById('away-title')
    awayTitle.innerText = data.awayTeam.name

    let percentHome = document.getElementById('percent-home')
    percentHome.innerHTML = (data.odds.homeWin / total * 100).toFixed(2) + '%'

    let percentAway = document.getElementById('percent-away')
    percentAway.innerHTML = (data.odds.awayWin / total * 100).toFixed(2) + '%'

    let percentDraw = document.getElementById('percent-draw')
    percentDraw.innerText = (data.odds.draw / total * 100).toFixed(2) + '%'
}

const getDate = (day) => {
    const dateString = day;
    const date = new Date(dateString);
    
    const options = { year: 'numeric', month: 'short', day: 'numeric' };
    const formattedDate = date.toLocaleDateString('en-US', options);
    return formattedDate
}

