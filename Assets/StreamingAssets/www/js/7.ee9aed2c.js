(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([[7],{"2f36":function(t,e,n){"use strict";n.r(e);var a=function(){var t=this,e=t.$createElement,n=t._self._c||e;return n("q-page",{staticClass:"q-pa-md"},[n("q-timeline",{attrs:{color:"secondary"}},[n("q-timeline-entry",{attrs:{heading:"",tag:"h4"}},[t._v("\n      "+t._s(t.data.name)+" "),n("q-chip",{attrs:{color:"grey-9","text-color":"white",label:t.getDurationInMinutesAsLabel(t.getTotalDuration())}})],1),n("q-separator"),n("q-timeline-entry",{attrs:{heading:"",tag:"p"}},[t._v("\n      "+t._s(t.data.description)+"\n    ")]),n("q-timeline-entry",{attrs:{heading:"",tag:"h6"}},[t._v("\n      Exercises in this training\n    ")]),t._l(t.data.exercises,(function(e){return n("q-timeline-entry",{key:"sm-"+e,attrs:{title:e.name,icon:t.getIcon(e),color:t.getColor(e),subtitle:t.getDurationInMinutesAsLabel(e.durationInSeconds),body:e.description}})}))],2),n("q-footer",{staticClass:"bg-grey-10 q-pa-sm text-center",attrs:{bordered:""}},[n("div",{staticClass:"text-h6"},[t._v("Continue on the screen "),n("q-icon",{attrs:{name:"airplay"}})],1),n("div",{staticClass:"text-caption"},[t._v("You don't need your smartphone during training")])]),n("q-inner-loading",{attrs:{showing:t.loading}},[n("q-spinner-ios",{attrs:{size:"30px",color:"white"}})],1)],1)},r=[],i={name:"Training",data(){return{checkIfTrainingIsActive:!0,interval:null}},computed:{loading:{get(){return this.$store.state.data.loading}},data:{get(){return this.$store.state.data.currentTraining}}},created(){this.$store.dispatch("data/getCurrentTraining")},methods:{getDurationInMinutesAsLabel:function(t){var e=Math.floor(t/60),n=t%60;return(0!==e?e+" min":"")+(0!==n?" "+n+" sec":"")},getTotalDuration:function(){var t=0;return this.data.exercises.forEach((e=>{t+=e.durationInSeconds})),t},getIcon:function(t){return"pause"===t.type?"pause":null},getColor:function(t){return"pause"===t.type?"blue-grey-9":null}}},o=i,s=n("2877"),l=n("9989"),c=n("05eb"),u=n("74af"),d=n("b047"),g=n("eb85"),p=n("7ff0"),h=n("0016"),m=n("74f7"),f=n("d9b2"),b=n("eebe"),y=n.n(b),q=Object(s["a"])(o,a,r,!1,null,null,null);e["default"]=q.exports;y()(q,"components",{QPage:l["a"],QTimeline:c["a"],QTimelineEntry:u["a"],QChip:d["a"],QSeparator:g["a"],QFooter:p["a"],QIcon:h["a"],QInnerLoading:m["a"],QSpinnerIos:f["a"]})}}]);