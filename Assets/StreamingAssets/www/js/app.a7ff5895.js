(function(t){function e(e){for(var a,o,s=e[0],c=e[1],u=e[2],l=0,d=[];l<s.length;l++)o=s[l],Object.prototype.hasOwnProperty.call(r,o)&&r[o]&&d.push(r[o][0]),r[o]=0;for(a in c)Object.prototype.hasOwnProperty.call(c,a)&&(t[a]=c[a]);f&&f(e);while(d.length)d.shift()();return i.push.apply(i,u||[]),n()}function n(){for(var t,e=0;e<i.length;e++){for(var n=i[e],a=!0,o=1;o<n.length;o++){var s=n[o];0!==r[s]&&(a=!1)}a&&(i.splice(e--,1),t=c(c.s=n[0]))}return t}var a={},o={1:0},r={1:0},i=[];function s(t){return c.p+"js/"+({}[t]||t)+"."+{2:"7dc24232",3:"d932e708",4:"b9830bd3",5:"6aca44d5",6:"6eafb325",7:"ee9aed2c"}[t]+".js"}function c(e){if(a[e])return a[e].exports;var n=a[e]={i:e,l:!1,exports:{}};return t[e].call(n.exports,n,n.exports,c),n.l=!0,n.exports}c.e=function(t){var e=[],n={2:1};o[t]?e.push(o[t]):0!==o[t]&&n[t]&&e.push(o[t]=new Promise((function(e,n){for(var a="css/"+({}[t]||t)+"."+{2:"6bf7a847",3:"31d6cfe0",4:"31d6cfe0",5:"31d6cfe0",6:"31d6cfe0",7:"31d6cfe0"}[t]+".css",r=c.p+a,i=document.getElementsByTagName("link"),s=0;s<i.length;s++){var u=i[s],l=u.getAttribute("data-href")||u.getAttribute("href");if("stylesheet"===u.rel&&(l===a||l===r))return e()}var d=document.getElementsByTagName("style");for(s=0;s<d.length;s++){u=d[s],l=u.getAttribute("data-href");if(l===a||l===r)return e()}var f=document.createElement("link");f.rel="stylesheet",f.type="text/css",f.onload=e,f.onerror=function(e){var a=e&&e.target&&e.target.src||r,i=new Error("Loading CSS chunk "+t+" failed.\n("+a+")");i.code="CSS_CHUNK_LOAD_FAILED",i.request=a,delete o[t],f.parentNode.removeChild(f),n(i)},f.href=r;var p=document.getElementsByTagName("head")[0];p.appendChild(f)})).then((function(){o[t]=0})));var a=r[t];if(0!==a)if(a)e.push(a[2]);else{var i=new Promise((function(e,n){a=r[t]=[e,n]}));e.push(a[2]=i);var u,l=document.createElement("script");l.charset="utf-8",l.timeout=120,c.nc&&l.setAttribute("nonce",c.nc),l.src=s(t);var d=new Error;u=function(e){l.onerror=l.onload=null,clearTimeout(f);var n=r[t];if(0!==n){if(n){var a=e&&("load"===e.type?"missing":e.type),o=e&&e.target&&e.target.src;d.message="Loading chunk "+t+" failed.\n("+a+": "+o+")",d.name="ChunkLoadError",d.type=a,d.request=o,n[1](d)}r[t]=void 0}};var f=setTimeout((function(){u({type:"timeout",target:l})}),12e4);l.onerror=l.onload=u,document.head.appendChild(l)}return Promise.all(e)},c.m=t,c.c=a,c.d=function(t,e,n){c.o(t,e)||Object.defineProperty(t,e,{enumerable:!0,get:n})},c.r=function(t){"undefined"!==typeof Symbol&&Symbol.toStringTag&&Object.defineProperty(t,Symbol.toStringTag,{value:"Module"}),Object.defineProperty(t,"__esModule",{value:!0})},c.t=function(t,e){if(1&e&&(t=c(t)),8&e)return t;if(4&e&&"object"===typeof t&&t&&t.__esModule)return t;var n=Object.create(null);if(c.r(n),Object.defineProperty(n,"default",{enumerable:!0,value:t}),2&e&&"string"!=typeof t)for(var a in t)c.d(n,a,function(e){return t[e]}.bind(null,a));return n},c.n=function(t){var e=t&&t.__esModule?function(){return t["default"]}:function(){return t};return c.d(e,"a",e),e},c.o=function(t,e){return Object.prototype.hasOwnProperty.call(t,e)},c.p="",c.oe=function(t){throw console.error(t),t};var u=window["webpackJsonp"]=window["webpackJsonp"]||[],l=u.push.bind(u);u.push=e,u=u.slice();for(var d=0;d<u.length;d++)e(u[d]);var f=l;i.push([0,0]),n()})({0:function(t,e,n){t.exports=n("2f39")},"0047":function(t,e,n){},"2f39":function(t,e,n){"use strict";n.r(e);var a={};n.r(a),n.d(a,"isBackButtonAvailable",(function(){return N})),n.d(a,"getClientAppState",(function(){return C})),n.d(a,"getUserId",(function(){return U}));var o={};n.r(o),n.d(o,"setBackButtonAvailable",(function(){return E})),n.d(o,"setClientAppState",(function(){return L})),n.d(o,"setLoading",(function(){return k})),n.d(o,"setTrainingsData",(function(){return P})),n.d(o,"setCurrentTraining",(function(){return $})),n.d(o,"setTrainingResults",(function(){return B})),n.d(o,"setUserData",(function(){return D})),n.d(o,"setUserId",(function(){return x}));var r={};n.r(r),n.d(r,"getCurrentAppState",(function(){return j})),n.d(r,"getUserId",(function(){return Q})),n.d(r,"logInUser",(function(){return F})),n.d(r,"backButtonClicked",(function(){return M})),n.d(r,"getTrainings",(function(){return J})),n.d(r,"getUserData",(function(){return V})),n.d(r,"getCurrentTraining",(function(){return W})),n.d(r,"getTrainingResults",(function(){return H})),n.d(r,"selectTraining",(function(){return Y})),n.d(r,"cancelTraining",(function(){return z}));n("e6cf"),n("5319"),n("7d6e"),n("e54f"),n("4439"),n("4605"),n("f580"),n("5b2b"),n("8753"),n("2967"),n("7e67"),n("d770"),n("dd82"),n("922c"),n("d7fb"),n("a533"),n("c32e"),n("a151"),n("8bc7"),n("e80f"),n("5fec"),n("e42f"),n("57fc"),n("d67f"),n("880e"),n("1c10"),n("9482"),n("e797"),n("4848"),n("53d0"),n("63b1"),n("e9fd"),n("195c"),n("64e9"),n("33c5"),n("4f62"),n("0dbc"),n("7c38"),n("0756"),n("4953"),n("81db"),n("2e52"),n("2248"),n("7797"),n("12a1"),n("ce96"),n("70ca"),n("2318"),n("24bd"),n("8f27"),n("3064"),n("c9a2"),n("8767"),n("4a8e"),n("b828"),n("3c1c"),n("21cb"),n("c00e"),n("e4a8"),n("e4d3"),n("f4d9"),n("fffd"),n("f645"),n("639e"),n("34ee"),n("b794"),n("af24"),n("7c9c"),n("7bb2"),n("64f7"),n("c382"),n("053c"),n("c48f"),n("f5d1"),n("3cec"),n("c00ee"),n("d450"),n("ca07"),n("14e3"),n("9393"),n("9227"),n("1dba"),n("674a"),n("de26"),n("6721"),n("9cb5"),n("ed9b"),n("fc83"),n("98e5"),n("605a"),n("ba60"),n("df07"),n("7903"),n("e046"),n("58af"),n("7713"),n("0571"),n("3e27"),n("6837"),n("3fc9"),n("0693"),n("bf41"),n("985d"),n("0047");var i=n("2b0e"),s=n("1f91"),c=n("42d2"),u=n("b05d");i["a"].use(u["a"],{config:{},lang:s["a"],iconSet:c["a"]});var l=function(){var t=this,e=t.$createElement,n=t._self._c||e;return n("div",{attrs:{id:"q-app"}},[n("router-view"),n("q-dialog",{attrs:{persistent:""},model:{value:t.showRegisterNewUser,callback:function(e){t.showRegisterNewUser=e},expression:"showRegisterNewUser"}},[n("q-card",[n("q-card-section",{staticClass:"row items-center"},[n("div",{staticClass:"text-h6"},[t._v("Hi! What's your name?")]),n("div",[t._v("When providing a name, you will see a personal message on the screen when your device has been successfully connected.")])]),n("q-card-section",{staticClass:"row items-center"},[n("q-input",{staticClass:"full-width",attrs:{filled:"",label:"Your name"},model:{value:t.name,callback:function(e){t.name=e},expression:"name"}})],1),n("q-card-actions",{attrs:{align:"around"}},[n("q-btn",{directives:[{name:"close-popup",rawName:"v-close-popup"}],attrs:{flat:"",label:"Continue",color:"primary"},on:{click:function(e){return t.createUser()}}})],1)],1)],1)],1)},d=[],f={name:"App",created(){this.interval=setInterval((()=>this.getCurrentAppState()),1e3),this.logInUser()},mounted(){},data(){return{showRegisterNewUser:!1,name:""}},computed:{state:{get(){return this.$store.state.data.clientAppState}},currentRouteName(){return this.$route.name},userId:{get(){return this.$store.state.data.userId},set(t){this.$store.commit("data/setUserId",t)}},userData:{get(){return this.$store.state.data.userData},set(t){this.$store.commit("data/setUserData",t)}}},watch:{userId(t){localStorage.userId=this.userId,this.getUserData()},userData(t){localStorage.userData=JSON.stringify(this.userData)}},methods:{getCurrentAppState:function(){this.$store.dispatch("data/getCurrentAppState"),this.routeBasedOnState()},createUser:function(){this.$store.dispatch("data/getUserId",this.name)},logInUser:function(){null!=localStorage.userData?this.$store.dispatch("data/logInUser",localStorage.userData):this.showRegisterNewUser=!0},getUserData:function(){null!=this.userId?this.$store.dispatch("data/getUserData",this.userId):this.logInUser()},routeBasedOnState:function(){var t="";switch(this.state){case"START":t="Start",this.$store.commit("data/setBackButtonAvailable",!1);break;case"SELECTED_TRAINING":t="Training",this.$store.commit("data/setBackButtonAvailable",!0);break;case"IN_TRAINING":t="InTraining",this.$store.commit("data/setBackButtonAvailable",!1);break;case"POST_TRAINING":t="PostTraining",this.$store.commit("data/setBackButtonAvailable",!1);break;default:break}this.currentRouteName!==t&&(this.$router.push({name:t}),this.$store.commit("data/setLoading",!1))}}},p=f,h=n("2877"),g=n("24e8"),m=n("f09f"),b=n("a370"),v=n("27f9"),y=n("4b7e"),w=n("9c40"),T=n("7f67"),I=n("eebe"),S=n.n(I),A=Object(h["a"])(p,l,d,!1,null,null,null),R=A.exports;S()(A,"components",{QDialog:g["a"],QCard:m["a"],QCardSection:b["a"],QInput:v["a"],QCardActions:y["a"],QBtn:w["a"]}),S()(A,"directives",{ClosePopup:T["a"]});var O=n("2f62"),_=function(){return{backButtonAvailable:!1,clientAppState:null,loading:!0,trainings:[],currentTraining:{},trainingResults:{},userId:null,userData:null}};function N(t){return t.backButtonAvailable}function C(t){return t.clientAppState}function U(t){return t.userId}function E(t,e){t.backButtonAvailable=e}function L(t,e){t.clientAppState=e}function k(t,e){t.loading=e}function P(t,e){t.trainings=e.trainings}function $(t,e){t.currentTraining=e}function B(t,e){t.trainingResults=e}function D(t,e){t.userData=e}function x(t,e){console.log("Setting user ID to "+e),t.userId=e}var q=n("bc3a"),K=n.n(q);const G=location.protocol+"//"+location.hostname+":1234";async function j(t){var e="GET_APP_STATE";await K.a.post(G,e).then((t=>{"OK"===t.statusText?this.commit("data/setClientAppState",t.data):console.error("Response status is not OK: "+t)})).catch((()=>{this.$q.notify({color:"negative",position:"top",message:"Loading failed",icon:"report_problem"})}))}async function Q(t,e){var n="REGISTER_NEW_USER\n"+e;await K.a.post(G,n).then((t=>{"OK"===t.statusText?this.commit("data/setUserId",t.data):console.error("Response status is not OK: "+t)})).catch((()=>{this.$q.notify({color:"negative",position:"top",message:"Loading failed",icon:"report_problem"})}))}async function F(t,e){var n="LOGIN_USER\n"+e;console.log("Logging in: "+e),await K.a.post(G,n).then((t=>{"OK"===t.statusText?this.commit("data/setUserId",t.data):console.error("Response status is not OK: "+t)})).catch((()=>{this.$q.notify({color:"negative",position:"top",message:"Loading failed",icon:"report_problem"})}))}async function M(t){var e="UNSELECT_TRAINING";this.commit("data/setLoading",!0),X(e)}async function J(t){await K.a.post(G,"GET_TRAININGS").then((t=>{"OK"===t.statusText?this.commit("data/setTrainingsData",t.data):console.error("Response status is not OK: "+t),this.commit("data/setLoading",!1)})).catch((()=>{this.$q.notify({color:"negative",position:"top",message:"Loading failed",icon:"report_problem"})}))}async function V(t,e){var n="GET_USER\n"+e;await K.a.post(G,n).then((t=>{"OK"===t.statusText?this.commit("data/setUserData",t.data):console.error("Response status is not OK: "+t),this.commit("data/setLoading",!1)})).catch((()=>{this.$q.notify({color:"negative",position:"top",message:"Loading failed",icon:"report_problem"})}))}async function W(t){await K.a.post(G,"GET_TRAINING").then((t=>{"OK"===t.statusText?this.commit("data/setCurrentTraining",t.data):console.error("Response status is not OK: "+t),this.commit("data/setLoading",!1)})).catch((()=>{this.$q.notify({color:"negative",position:"top",message:"Loading failed",icon:"report_problem"})}))}async function H(t,e){console.log("Using user id "+e);var n="GET_RESULTS\n"+e;await K.a.post(G,n).then((t=>{console.log(t.data),"OK"===t.statusText?this.commit("data/setTrainingResults",t.data):console.error("Response status is not OK: "+t),this.commit("data/setLoading",!1)})).catch((()=>{this.$q.notify({color:"negative",position:"top",message:"Loading failed",icon:"report_problem"})}))}function Y(t,e){var n="SELECT_TRAINING\n"+e;this.commit("data/setLoading",!0),X(n)}function z(t,e){var n="CANCEL_TRAINING\n"+e;this.commit("data/setLoading",!0),X(n)}async function X(t){await K.a.post(G,t).then((e=>{this.commit("data/setLoading",!1),e.data?console.error("Response status is not OK: "+e):console.warn("Sending data: "+t+". Repsonse: "+e.data)})).catch((()=>{this.$q.notify({color:"negative",position:"top",message:"Loading app state failed",icon:"report_problem"})}))}var Z={namespaced:!0,state:_,getters:a,mutations:o,actions:r};i["a"].use(O["a"]);var tt=function(){const t=new O["a"].Store({modules:{data:Z},strict:!1});return t},et=n("8c4f");const nt=[{path:"/",component:()=>Promise.all([n.e(0),n.e(2)]).then(n.bind(null,"713b")),children:[{path:"",name:"Start",component:()=>Promise.all([n.e(0),n.e(5)]).then(n.bind(null,"8b24"))},{path:"/training/",name:"Training",component:()=>Promise.all([n.e(0),n.e(7)]).then(n.bind(null,"2f36"))},{path:"/in-training/",name:"InTraining",component:()=>Promise.all([n.e(0),n.e(4)]).then(n.bind(null,"af8f"))},{path:"/post-training/",name:"PostTraining",component:()=>Promise.all([n.e(0),n.e(6)]).then(n.bind(null,"7383"))}]},{path:"*",component:()=>n.e(3).then(n.bind(null,"ee5d"))}];var at=nt;i["a"].use(et["a"]);var ot=function(){const t=new et["a"]({scrollBehavior:()=>({x:0,y:0}),routes:at,mode:"hash",base:""});return t},rt=async function(){const t="function"===typeof tt?await tt({Vue:i["a"]}):tt,e="function"===typeof ot?await ot({Vue:i["a"],store:t}):ot;t.$router=e;const n={router:e,store:t,render:t=>t(R),el:"#q-app"};return{app:n,store:t,router:e}};i["a"].prototype.$axios=K.a;n("c975"),n("13d5");let it=R.options||R,st="function"===typeof it.preFetch;function ct(t,e){const n=t?t.matched?t:e.resolve(t).route:e.currentRoute;return n?Array.prototype.concat.apply([],n.matched.map((t=>Object.keys(t.components).map((e=>{const n=t.components[e];return{path:t.path,c:n.options||n}}))))):[]}function ut(t,e,n){t.beforeResolve(((a,o,r)=>{const i=window.location.href.replace(window.location.origin,""),s=ct(a,t),c=ct(o,t);let u=!1;const l=s.filter(((t,e)=>u||(u=!c[e]||c[e].c!==t.c||t.path.indexOf("/:")>-1))).filter((t=>t.c&&"function"===typeof t.c.preFetch)).map((t=>t.c.preFetch));if(!0===st&&(st=!1,l.unshift(it.preFetch)),0===l.length)return r();let d=!1;const f=t=>{d=!0,r(t)},p=()=>{!1===d&&r()};l.reduce(((t,r)=>t.then((()=>!1===d&&r({store:e,currentRoute:a,previousRoute:o,redirect:f,urlPath:i,publicPath:n})))),Promise.resolve()).then(p).catch((t=>{console.error(t),p()}))}))}const lt="";async function dt(){const{app:t,store:e,router:n}=await rt();let a=!1;const o=t=>{a=!0;const e=Object(t)===t?n.resolve(t).route.fullPath:t;window.location.href=e},r=window.location.href.replace(window.location.origin,""),s=[void 0];for(let u=0;!1===a&&u<s.length;u++)if("function"===typeof s[u])try{await s[u]({app:t,router:n,store:e,Vue:i["a"],ssrContext:null,redirect:o,urlPath:r,publicPath:lt})}catch(c){return c&&c.url?void(window.location.href=c.url):void console.error("[Quasar] boot error:",c)}!0!==a&&(ut(n,e),new i["a"](t))}dt()}});