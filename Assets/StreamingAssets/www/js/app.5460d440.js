(function(t){function e(e){for(var r,a,c=e[0],s=e[1],u=e[2],l=0,d=[];l<c.length;l++)a=c[l],Object.prototype.hasOwnProperty.call(o,a)&&o[a]&&d.push(o[a][0]),o[a]=0;for(r in s)Object.prototype.hasOwnProperty.call(s,r)&&(t[r]=s[r]);f&&f(e);while(d.length)d.shift()();return i.push.apply(i,u||[]),n()}function n(){for(var t,e=0;e<i.length;e++){for(var n=i[e],r=!0,a=1;a<n.length;a++){var c=n[a];0!==o[c]&&(r=!1)}r&&(i.splice(e--,1),t=s(s.s=n[0]))}return t}var r={},a={1:0},o={1:0},i=[];function c(t){return s.p+"js/"+({}[t]||t)+"."+{2:"7dc24232",3:"d932e708",4:"b9830bd3",5:"6aca44d5",6:"202ce1d3",7:"ee9aed2c"}[t]+".js"}function s(e){if(r[e])return r[e].exports;var n=r[e]={i:e,l:!1,exports:{}};return t[e].call(n.exports,n,n.exports,s),n.l=!0,n.exports}s.e=function(t){var e=[],n={2:1};a[t]?e.push(a[t]):0!==a[t]&&n[t]&&e.push(a[t]=new Promise((function(e,n){for(var r="css/"+({}[t]||t)+"."+{2:"6bf7a847",3:"31d6cfe0",4:"31d6cfe0",5:"31d6cfe0",6:"31d6cfe0",7:"31d6cfe0"}[t]+".css",o=s.p+r,i=document.getElementsByTagName("link"),c=0;c<i.length;c++){var u=i[c],l=u.getAttribute("data-href")||u.getAttribute("href");if("stylesheet"===u.rel&&(l===r||l===o))return e()}var d=document.getElementsByTagName("style");for(c=0;c<d.length;c++){u=d[c],l=u.getAttribute("data-href");if(l===r||l===o)return e()}var f=document.createElement("link");f.rel="stylesheet",f.type="text/css",f.onload=e,f.onerror=function(e){var r=e&&e.target&&e.target.src||o,i=new Error("Loading CSS chunk "+t+" failed.\n("+r+")");i.code="CSS_CHUNK_LOAD_FAILED",i.request=r,delete a[t],f.parentNode.removeChild(f),n(i)},f.href=o;var p=document.getElementsByTagName("head")[0];p.appendChild(f)})).then((function(){a[t]=0})));var r=o[t];if(0!==r)if(r)e.push(r[2]);else{var i=new Promise((function(e,n){r=o[t]=[e,n]}));e.push(r[2]=i);var u,l=document.createElement("script");l.charset="utf-8",l.timeout=120,s.nc&&l.setAttribute("nonce",s.nc),l.src=c(t);var d=new Error;u=function(e){l.onerror=l.onload=null,clearTimeout(f);var n=o[t];if(0!==n){if(n){var r=e&&("load"===e.type?"missing":e.type),a=e&&e.target&&e.target.src;d.message="Loading chunk "+t+" failed.\n("+r+": "+a+")",d.name="ChunkLoadError",d.type=r,d.request=a,n[1](d)}o[t]=void 0}};var f=setTimeout((function(){u({type:"timeout",target:l})}),12e4);l.onerror=l.onload=u,document.head.appendChild(l)}return Promise.all(e)},s.m=t,s.c=r,s.d=function(t,e,n){s.o(t,e)||Object.defineProperty(t,e,{enumerable:!0,get:n})},s.r=function(t){"undefined"!==typeof Symbol&&Symbol.toStringTag&&Object.defineProperty(t,Symbol.toStringTag,{value:"Module"}),Object.defineProperty(t,"__esModule",{value:!0})},s.t=function(t,e){if(1&e&&(t=s(t)),8&e)return t;if(4&e&&"object"===typeof t&&t&&t.__esModule)return t;var n=Object.create(null);if(s.r(n),Object.defineProperty(n,"default",{enumerable:!0,value:t}),2&e&&"string"!=typeof t)for(var r in t)s.d(n,r,function(e){return t[e]}.bind(null,r));return n},s.n=function(t){var e=t&&t.__esModule?function(){return t["default"]}:function(){return t};return s.d(e,"a",e),e},s.o=function(t,e){return Object.prototype.hasOwnProperty.call(t,e)},s.p="",s.oe=function(t){throw console.error(t),t};var u=window["webpackJsonp"]=window["webpackJsonp"]||[],l=u.push.bind(u);u.push=e,u=u.slice();for(var d=0;d<u.length;d++)e(u[d]);var f=l;i.push([0,0]),n()})({0:function(t,e,n){t.exports=n("2f39")},"0047":function(t,e,n){},"2f39":function(t,e,n){"use strict";n.r(e);var r={};n.r(r),n.d(r,"isBackButtonAvailable",(function(){return y})),n.d(r,"getClientAppState",(function(){return T})),n.d(r,"getUserId",(function(){return I}));var a={};n.r(a),n.d(a,"setBackButtonAvailable",(function(){return w})),n.d(a,"setClientAppState",(function(){return S})),n.d(a,"setLoading",(function(){return A})),n.d(a,"setTrainingsData",(function(){return _})),n.d(a,"setCurrentTraining",(function(){return E})),n.d(a,"setTrainingResults",(function(){return N})),n.d(a,"setUserId",(function(){return P}));var o={};n.r(o),n.d(o,"getCurrentAppState",(function(){return R})),n.d(o,"getUserId",(function(){return B})),n.d(o,"backButtonClicked",(function(){return $})),n.d(o,"getTrainings",(function(){return O})),n.d(o,"getCurrentTraining",(function(){return U})),n.d(o,"getTrainingResults",(function(){return j})),n.d(o,"selectTraining",(function(){return G})),n.d(o,"cancelTraining",(function(){return x}));n("e6cf"),n("5319"),n("7d6e"),n("e54f"),n("4439"),n("4605"),n("f580"),n("5b2b"),n("8753"),n("2967"),n("7e67"),n("d770"),n("dd82"),n("922c"),n("d7fb"),n("a533"),n("c32e"),n("a151"),n("8bc7"),n("e80f"),n("5fec"),n("e42f"),n("57fc"),n("d67f"),n("880e"),n("1c10"),n("9482"),n("e797"),n("4848"),n("53d0"),n("63b1"),n("e9fd"),n("195c"),n("64e9"),n("33c5"),n("4f62"),n("0dbc"),n("7c38"),n("0756"),n("4953"),n("81db"),n("2e52"),n("2248"),n("7797"),n("12a1"),n("ce96"),n("70ca"),n("2318"),n("24bd"),n("8f27"),n("3064"),n("c9a2"),n("8767"),n("4a8e"),n("b828"),n("3c1c"),n("21cb"),n("c00e"),n("e4a8"),n("e4d3"),n("f4d9"),n("fffd"),n("f645"),n("639e"),n("34ee"),n("b794"),n("af24"),n("7c9c"),n("7bb2"),n("64f7"),n("c382"),n("053c"),n("c48f"),n("f5d1"),n("3cec"),n("c00ee"),n("d450"),n("ca07"),n("14e3"),n("9393"),n("9227"),n("1dba"),n("674a"),n("de26"),n("6721"),n("9cb5"),n("ed9b"),n("fc83"),n("98e5"),n("605a"),n("ba60"),n("df07"),n("7903"),n("e046"),n("58af"),n("7713"),n("0571"),n("3e27"),n("6837"),n("3fc9"),n("0693"),n("bf41"),n("985d"),n("0047");var i=n("2b0e"),c=n("1f91"),s=n("42d2"),u=n("b05d");i["a"].use(u["a"],{config:{},lang:c["a"],iconSet:s["a"]});var l=function(){var t=this,e=t.$createElement,n=t._self._c||e;return n("div",{attrs:{id:"q-app"}},[n("router-view")],1)},d=[],f={name:"App",created(){this.interval=setInterval((()=>this.getCurrentAppState()),1e3),this.checkOrCreateUserId()},mounted(){localStorage.userId&&this.$store.commit("data/setUserId",localStorage.userId)},computed:{state:{get(){return this.$store.state.data.clientAppState}},currentRouteName(){return this.$route.name},userId:{get(){return this.$store.state.data.userId},set(t){this.$store.commit("data/setUserId",t)}}},watch:{userId(t){localStorage.userId=this.userId}},methods:{getCurrentAppState:function(){this.$store.dispatch("data/getCurrentAppState"),this.routeBasedOnState()},checkOrCreateUserId:function(){null==this.userId&&null==localStorage.userId&&this.$store.dispatch("data/getUserId")},routeBasedOnState:function(){var t="";switch(this.state){case"START":t="Start",this.$store.commit("data/setBackButtonAvailable",!1);break;case"SELECTED_TRAINING":t="Training",this.$store.commit("data/setBackButtonAvailable",!0);break;case"IN_TRAINING":t="InTraining",this.$store.commit("data/setBackButtonAvailable",!1);break;case"POST_TRAINING":t="PostTraining",this.$store.commit("data/setBackButtonAvailable",!1);break;default:break}this.currentRouteName!==t&&(this.$router.push({name:t}),this.$store.commit("data/setLoading",!1))}}},p=f,h=n("2877"),g=Object(h["a"])(p,l,d,!1,null,null,null),m=g.exports,b=n("2f62"),v=function(){return{backButtonAvailable:!1,clientAppState:null,loading:!0,trainings:[],currentTraining:{},trainingResults:{},userId:null,userData:{}}};function y(t){return t.backButtonAvailable}function T(t){return t.clientAppState}function I(t){return t.userId}function w(t,e){t.backButtonAvailable=e}function S(t,e){t.clientAppState=e}function A(t,e){t.loading=e}function _(t,e){t.trainings=e.trainings}function E(t,e){t.currentTraining=e}function N(t,e){t.trainingResults=e}function P(t,e){console.log("Setting user ID to "+e),t.userId=e}var k=n("bc3a"),C=n.n(k);const L=location.protocol+"//"+location.hostname+":1234";async function R(t){var e="GET_APP_STATE";await C.a.post(L,e).then((t=>{this.commit("data/setClientAppState",t.data)})).catch((()=>{this.$q.notify({color:"negative",position:"top",message:"Loading failed",icon:"report_problem"})}))}async function B(t){var e="REGISTER_NEW_USER";await C.a.post(L,e).then((t=>{this.commit("data/setUserId",t.data)})).catch((()=>{this.$q.notify({color:"negative",position:"top",message:"Loading failed",icon:"report_problem"})}))}async function $(t){var e="UNSELECT_TRAINING";this.commit("data/setLoading",!0),q(e)}async function O(t){await C.a.post(L,"GET_TRAININGS").then((t=>{this.commit("data/setTrainingsData",t.data),this.commit("data/setLoading",!1)})).catch((()=>{this.$q.notify({color:"negative",position:"top",message:"Loading failed",icon:"report_problem"})}))}async function U(t){await C.a.post(L,"GET_TRAINING").then((t=>{this.commit("data/setCurrentTraining",t.data),this.commit("data/setLoading",!1)})).catch((()=>{this.$q.notify({color:"negative",position:"top",message:"Loading failed",icon:"report_problem"})}))}async function j(t){console.log("Using user id "+t.getters.getUserId);var e="GET_RESULTS\n"+t.getters.getUserId;await C.a.post(L,e).then((t=>{console.log(t.data),this.commit("data/setTrainingResults",t.data),this.commit("data/setLoading",!1)})).catch((()=>{this.$q.notify({color:"negative",position:"top",message:"Loading failed",icon:"report_problem"})}))}function G(t,e){var n="SELECT_TRAINING\n"+e;this.commit("data/setLoading",!0),q(n)}function x(t,e){var n="CANCEL_TRAINING\n"+e;this.commit("data/setLoading",!0),q(n)}async function q(t){await C.a.post(L,t).then((e=>{this.commit("data/setLoading",!1),e.data||console.warn("Sending data: "+t+". Repsonse: "+e.data)})).catch((()=>{this.$q.notify({color:"negative",position:"top",message:"Loading app state failed",icon:"report_problem"})}))}var D={namespaced:!0,state:v,getters:r,mutations:a,actions:o};i["a"].use(b["a"]);var F=function(){const t=new b["a"].Store({modules:{data:D},strict:!1});return t},M=n("8c4f");const V=[{path:"/",component:()=>Promise.all([n.e(0),n.e(2)]).then(n.bind(null,"713b")),children:[{path:"",name:"Start",component:()=>Promise.all([n.e(0),n.e(5)]).then(n.bind(null,"8b24"))},{path:"/training/",name:"Training",component:()=>Promise.all([n.e(0),n.e(7)]).then(n.bind(null,"2f36"))},{path:"/in-training/",name:"InTraining",component:()=>Promise.all([n.e(0),n.e(4)]).then(n.bind(null,"af8f"))},{path:"/post-training/",name:"PostTraining",component:()=>Promise.all([n.e(0),n.e(6)]).then(n.bind(null,"7383"))}]},{path:"*",component:()=>Promise.all([n.e(0),n.e(3)]).then(n.bind(null,"ee5d"))}];var J=V;i["a"].use(M["a"]);var H=function(){const t=new M["a"]({scrollBehavior:()=>({x:0,y:0}),routes:J,mode:"hash",base:""});return t},K=async function(){const t="function"===typeof F?await F({Vue:i["a"]}):F,e="function"===typeof H?await H({Vue:i["a"],store:t}):H;t.$router=e;const n={router:e,store:t,render:t=>t(m),el:"#q-app"};return{app:n,store:t,router:e}};i["a"].prototype.$axios=C.a;n("c975"),n("13d5");let Q=m.options||m,W="function"===typeof Q.preFetch;function z(t,e){const n=t?t.matched?t:e.resolve(t).route:e.currentRoute;return n?Array.prototype.concat.apply([],n.matched.map((t=>Object.keys(t.components).map((e=>{const n=t.components[e];return{path:t.path,c:n.options||n}}))))):[]}function X(t,e,n){t.beforeResolve(((r,a,o)=>{const i=window.location.href.replace(window.location.origin,""),c=z(r,t),s=z(a,t);let u=!1;const l=c.filter(((t,e)=>u||(u=!s[e]||s[e].c!==t.c||t.path.indexOf("/:")>-1))).filter((t=>t.c&&"function"===typeof t.c.preFetch)).map((t=>t.c.preFetch));if(!0===W&&(W=!1,l.unshift(Q.preFetch)),0===l.length)return o();let d=!1;const f=t=>{d=!0,o(t)},p=()=>{!1===d&&o()};l.reduce(((t,o)=>t.then((()=>!1===d&&o({store:e,currentRoute:r,previousRoute:a,redirect:f,urlPath:i,publicPath:n})))),Promise.resolve()).then(p).catch((t=>{console.error(t),p()}))}))}const Y="";async function Z(){const{app:t,store:e,router:n}=await K();let r=!1;const a=t=>{r=!0;const e=Object(t)===t?n.resolve(t).route.fullPath:t;window.location.href=e},o=window.location.href.replace(window.location.origin,""),c=[void 0];for(let u=0;!1===r&&u<c.length;u++)if("function"===typeof c[u])try{await c[u]({app:t,router:n,store:e,Vue:i["a"],ssrContext:null,redirect:a,urlPath:o,publicPath:Y})}catch(s){return s&&s.url?void(window.location.href=s.url):void console.error("[Quasar] boot error:",s)}!0!==r&&(X(n,e),new i["a"](t))}Z()}});