<template>
    <div>
        <!--<h1>About PubHub</h1>
        <v-card class="about-us">
            <v-card-text>
            A little about me here! This is placeholder text that fills out the card to give an idea of how it will look when you've actually completed your bio. A little about me here! This is placeholder text that fills out the card to give an idea of how it will look when you've actually completed your bio.
            </v-card-text>
        </v-card>-->

        <h1>About Us</h1>
        <div
            style="
                display: flex;
                justify-content: space-between;
                flex-wrap: wrap;
            "
        >
            <v-card class="about-us">
                <img src="@/assets/ryan.jpg"/>
                <v-card-title>Ryan DuPerow</v-card-title>
                <v-card-text>
                    <p>
                        Thanks for checking out PubHub!  My name is Ryan, and when
                        I'm not sampling some of Cincinnati's finest Craft Beers 
                        ("market research"), you can find me playing Roblox with my son or 
                        working to improve my Software Development skills.
                    </p>
                    <div style="display: flex">
                        <a target="blank" href="https://github.com/ryanduperow">
                            <v-icon color="primary">mdi-github</v-icon>
                            GitHub
                        </a>
                        <a target="blank" href="https://www.linkedin.com/in/ryanduperow/">
                            <v-icon color="primary">mdi-linkedin</v-icon>
                            LinkedIn
                        </a>
                    </div>
                </v-card-text>
            </v-card>

            <v-card class="about-us">
                <img src="@/assets/mustafa.jpg"/>
                <v-card-title>Mustafa Abuhilal</v-card-title>
                <v-card-text>
                    <p>
                        A little about me here! This is placeholder text that
                        fills out the card to give an idea of how it will look
                        when you've actually completed your bio.
                    </p>
                    <div style="display: flex">
                        <a target="blank" href="https://google.com">
                            <v-icon color="primary">mdi-github</v-icon>
                            GitHub
                        </a>
                        <a target="blank" href="https://google.com">
                            <v-icon color="primary">mdi-linkedin</v-icon>
                            LinkedIn
                        </a>
                    </div>
                </v-card-text>
            </v-card>

            <v-card class="about-us">
                <img src="@/assets/tyson.jpg"/>
                <v-card-title>Tyson Thorp</v-card-title>
                <v-card-text>
                    <p>
                        The Brewery Bros. would like to welcome you to PubHub!
                        Hopefully beer goggles won't prevent you from finding 
                        all the best craft breweries in the Cincinnati area.
                        Tap the keg on our stout local beer scene and hop to it!
                    </p>
                    <p>
                        An amateur conniseur of craft beer, when I'm not sampling a variety of local brews,
                        you can find me playing LEGOs with my daughters or taking my dog on long walks. 
                    </p>
                    <div style="display: flex">
                        <a target="blank" href="https://github.com/TysonThorp/">
                            <v-icon color="primary">mdi-github</v-icon>
                            GitHub
                        </a>
                        <a target="blank" href="https://www.linkedin.com/in/tysonthorp/">
                            <v-icon color="primary">mdi-linkedin</v-icon>
                            LinkedIn
                        </a>
                    </div>
                </v-card-text>
            </v-card>

            <v-card class="about-us">
                <img src="@/assets/levi.png"/>
                <v-card-title>Levi Swartz</v-card-title>
                <v-card-text>
                    <p>
                        Hi! Beginning life as a simple 'span' element, I learned CSS in order to style myself and take on the appearance of a real human being.</p>
                    <p>
                        Apart from helping to create the most stylish brewery finder the world has ever seen, I enjoy science fiction & fantasy novels, the music of 'NSP', playing ttrpgs with friends, and baking.
                    </p>
                    <p>Brew on!</p>
                    <div style="display: flex">
                        <a target="blank" href="https://github.com/Iniquit">
                            <v-icon color="primary">mdi-github</v-icon>
                            GitHub
                        </a>
                        <a target="blank" href="https://www.linkedin.com/in/elswartz/">
                            <v-icon color="primary">mdi-linkedin</v-icon>
                            LinkedIn
                        </a>
                    </div>
                </v-card-text>
            </v-card>
        </div>
    </div>
</template>

<script>
export default {};
</script>

<style scoped>
.about-us {
    margin-bottom: 1em;
    flex-basis: 50%;
    flex-grow: 1;
}
a {
    margin-right: 1em;
    display: flex;
    align-content: center;
    text-decoration: none;
}
img {
    object-fit: cover;
    max-height: 12em;
    width: 100%;
    border-radius: 4px;
}
</style>