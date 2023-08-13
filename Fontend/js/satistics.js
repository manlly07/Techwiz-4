const domain = 'https://localhost:7023/'

const fetchData = async (club, year) => {
    let table = document.getElementById('satistics-list')
    let oldTBody = table.querySelector('tbody')
    if(oldTBody) {
        table.removeChild(oldTBody)
    }

    const animate = document.getElementById("animate")
    animate.classList.remove('visually-hidden')

    const response = await fetch(`${domain}api/match?club=${club}&year=${year}`, 
    {
        method: 'GET',
    }).then(response => response.json())
    console.log(response);
    let standings = response.standings[0].table
    console.log(standings);

    animate.classList.add('visually-hidden')

    let newTBody = document.createElement('tbody')
    table.appendChild(newTBody)
    let htmls = standings.map(standing => {
        return (
            `
                <tr key="${standing.team.name}">
                    <td class="col-3 border-end">
                        <span class="mx-2">${standing.position}</span>
                        <img src="${standing.team.crest}" alt="" width="24" height="24">
                        <a href="./satistics_club.html?id=${standing.team.id}" class="text-primary mx-2">${standing.team.name}</a>
                    </td>
                    <td class="text-end">${standing.playedGames}</td>
                    <td class="text-end">${standing.won}</td>
                    <td class="text-end">${standing.draw}</td>
                    <td class="text-end">${standing.lost}</td>
                    <td class="text-end">${standing.goalsFor}</td>
                    <td class="text-end">${standing.goalsAgainst}</td>
                    <td class="text-end">${standing.goalDifference}</td>
                    <td class="text-end">${standing.points}</td>
                </tr>
            `
        )
    })
    newTBody.innerHTML = htmls.join('')
}
fetchData('PL', 2023)

const selectClub = document.getElementById("select-club-standing")
selectClub.addEventListener('change', (e) => {
    const selectedValue = e.target.value;
    const selectedText = e.target.options[e.target.selectedIndex].text;
    const selectYear = document.getElementById("select-club-year")
    const yearTitle = selectYear.options[selectYear.selectedIndex].textContent
    const year = selectYear.options[selectYear.selectedIndex].value
    console.log(year);
    console.log(selectedText);
    console.log(selectedValue)
    const competitionTitle = document.getElementById('competition-title')
    competitionTitle.innerText = selectedText + " " +yearTitle
    fetchData(selectedValue, year)
})

const selectYear = document.getElementById("select-club-year")
selectYear.addEventListener('change', (e) => {
    const selectedValue = e.target.value;
    const selectedText = e.target.options[e.target.selectedIndex].text;
    const selectClub = document.getElementById("select-club-standing")
    const clubTitle = selectClub.options[selectClub.selectedIndex].textContent
    const club = selectClub.options[selectClub.selectedIndex].value
    console.log(clubTitle);
    console.log(selectedText);
    console.log(selectedValue)
    const competitionTitle = document.getElementById('competition-title')
    competitionTitle.innerText = clubTitle + " " + selectedText
    fetchData(club, selectedValue)
})

const fetchNews = async (url, method, callback, data) => {

    let response = await fetch(url, 
    {
        method: method ?? 'GET',
    },
    data ? JSON.stringify(data) : ''
    )
    response  = await response.json()
    callback ? callback(response.data) : ''
    console.log(response)
};
// http://api.techwarriors.click/api/Article/GetList?PageSize=10&CurrentPage=1
// const GetArticleOnSatistics = (data) => {
//     let parent = document.getElementById('news-satistics')
//     let html = data.slice(0,2).map((item, index) => {
//         return `
//             <div class="blog-item-satistics col-lg-8 col-md-6 col-12 mb-30 row">
//                 <div class="content">
//                     <h3 class="title">
//                         <a href="#"
//                         >${item.title}</a
//                         >
//                     </h3>
//                     <p class="text-overflow">
//                         ${item.content}
//                     </p>
//                     <a href="#" class="read-more">READ MORE</a>
//                 </div>
//             </div>
//         `
//     })
//     if (parent) {
//         parent.innerHTML = html.join('')
//     }
// }
// fetchNews(`http://api.techwarriors.click/api/Article/GetList?PageSize=10&CurrentPage=1`, 'GET', GetArticleOnSatistics)
