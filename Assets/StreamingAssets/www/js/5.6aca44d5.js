(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([[5],{"8b24":function(t,a,n){"use strict";n.r(a);var i=function(){var t=this,a=t.$createElement,n=t._self._c||a;return n("q-page",{staticClass:"q-pa-md"},[n("div",[n("h4",[t._v("\n      Choose a training\n    ")]),n("div",{staticClass:"row wrap"},t._l(t.trainings,(function(a){return n("div",{key:"sm-"+a,staticClass:"col-xs-12 col-s-6 col-md-4 q-pa-xs"},[n("q-card",{staticClass:"trainings-card"},[n("q-card-section",[n("div",{staticClass:"text-h6"},[t._v(t._s(a.name))]),n("div",{staticClass:"text-subtitle2"},[t._v(t._s(t.getDurationInMinutesAsLabel(a.durationInSeconds)))])]),n("q-separator"),n("q-card-actions",{attrs:{vertical:""}},[n("q-btn",{attrs:{flat:""},on:{click:function(n){return t.openTraining(a)}}},[t._v("Launch training")])],1)],1)],1)})),0)]),n("q-inner-loading",{attrs:{showing:t.loading}},[n("q-spinner-ios",{attrs:{size:"30px",color:"white"}})],1)],1)},s=[],e=n("2b0e"),r=n("bc3a"),o=n.n(r);e["a"].prototype.$axios=o.a;var c={name:"Start",computed:{loading:{get(){return this.$store.state.data.loading}},trainings:{get(){return this.$store.state.data.trainings}}},beforeMount(){this.$store.dispatch("data/getTrainings")},methods:{openTraining:function(t){this.$store.dispatch("data/selectTraining",t.id)},getDurationInMinutesAsLabel:function(t){var a=Math.floor(t/60),n=t%60;return(0!==a?a+" min":"")+(0!==n?" "+n+" sec":"")}}},d=c,l=n("2877"),u=n("9989"),g=n("f09f"),p=n("a370"),h=n("eb85"),b=n("4b7e"),f=n("9c40"),v=n("74f7"),m=n("d9b2"),q=n("eebe"),w=n.n(q),C=Object(l["a"])(d,i,s,!1,null,null,null);a["default"]=C.exports;w()(C,"components",{QPage:u["a"],QCard:g["a"],QCardSection:p["a"],QSeparator:h["a"],QCardActions:b["a"],QBtn:f["a"],QInnerLoading:v["a"],QSpinnerIos:m["a"]})}}]);